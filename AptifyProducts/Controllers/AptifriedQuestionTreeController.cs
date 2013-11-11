using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
	public class AptifriedQuestionTreeController : AptifyEnabledApiController {
		private static int USER_ENTERED_KNOWLEDGE_ANSWER_ID = 45;
		private static int HEADER_QUESTION_TYPE = 1;

		public AptifriedQuestionTreeController(ISession session) : base(session) { }

		public IList<AptifriedQuestionTreeDto> Get() {
			var queryString = Request.RequestUri.Query;

			ICriteria queryCriteria = session.CreateCriteria<AptifriedQuestionTree>();

			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = session.ODataQuery<AptifriedQuestionTree>(queryString);
			} catch (ODataException odataException) {
				throw new HttpException(500, "Homie don't play that.", odataException);
			}

			return Mapper.Map(queryCriteria.List<AptifriedQuestionTree>(), new List<AptifriedQuestionTreeDto>());
		}

		public IList<AptifriedQuestionTree> GetById(int id) {
			return session.QueryOver<AptifriedQuestionTree>()
				.Where(x => x.Id == id)
				.List<AptifriedQuestionTree>();
		}

		[Authorize]
		public bool Post(AptifriedQuestionTreeSubmissionDto submissionDto) {
			HttpException ex = null;

			// Check for validity
			ex = CheckPostObject(submissionDto);
			if (ex != null) {
				throw ex;
			}

			// Get the question tree (now that we know it's there)
			var questionTree = session.QueryOver<AptifriedQuestionTree>().Where(x => x.Id == submissionDto.QuestionTreeId).List<AptifriedQuestionTree>().FirstOrDefault<AptifriedQuestionTree>();

			// Handle multiple submissions (if allowed)
			ex = CheckMultiSubmit(submissionDto, questionTree);
			if (ex != null) {
				throw ex;
			}

			// Handle partial submissions (if allowed)
			ex = CheckPartialSubmit(submissionDto, questionTree);
			if (ex != null) {
				throw ex;
			}

			var resultController = new AptifriedKnowledgeResultController(session);
			if (resultController == null) {
				throw new HttpException(500, "Couldn't instantiate knowledge result controller");
			}

			var resultObject = PostResult(submissionDto, resultController);
			if (resultObject == null) {
				throw new HttpException(500, "Null result when posting a new knowledge result internally");
			}

			var resultDetailController = new AptifriedKnowledgeResultDetailController(session);
			if (resultDetailController == null) {
				throw new HttpException(500, "Couldn't instantiate knowledge result detail controller");
			}

			for (int i = 0; i < submissionDto.Questions.Count; i++) {
				if (submissionDto.Questions[i].QuestionType.Id != HEADER_QUESTION_TYPE) {
					var resultDetailObject = PostResultDetail(resultDetailController, submissionDto, i, resultObject);
					if (resultDetailObject == null) {
						throw new HttpException(500, "Null result when posting a new knowledge result detail internally");
					}
				}
			}

			return true;
		}

		private HttpException CheckPostObject(AptifriedQuestionTreeSubmissionDto submissionDto) {
			if (submissionDto == null) {
				return new HttpException(500, "Question tree submission DTO is null");
			}

			if (submissionDto.PersonId < 1) {
				return new HttpException(500, "No valid person ID");
			}

			if (submissionDto.QuestionTreeId < 1) {
				return new HttpException(500, "No valid question tree ID");
			}

			if (session.QueryOver<AptifriedQuestionTree>().Where(x => x.Id == submissionDto.QuestionTreeId).RowCount() != 1) {
				return new HttpException(500, "No question tree found given that question tree ID");
			}

			if (submissionDto.Questions == null || submissionDto.Answers == null) {
				return new HttpException(500, "Missing questions and/or answers");
			}

			if (submissionDto.Questions.Count != submissionDto.Answers.Count) {
				return new HttpException(500, "Mismatched number of questions and answers");
			}

			return null;
		}

		private HttpException CheckMultiSubmit(AptifriedQuestionTreeSubmissionDto submissionDto, AptifriedQuestionTree questionTree) {
			if (!questionTree.AllowDuplicates) {
				var previousCount = session.QueryOver<AptifriedKnowledgeResult>().Where(x => x.QuestionTreeId == questionTree.Id && x.Person.Id == submissionDto.PersonId).RowCount();

				if (previousCount > 0) {
					return new HttpException(500, "Duplicate question tree submission");
				}
			}

			return null;
		}

		private HttpException CheckPartialSubmit(AptifriedQuestionTreeSubmissionDto submissionDto, AptifriedQuestionTree questionTree) {
			if (!questionTree.AllowPartialCompletion && !submissionDto.IsComplete) {
				return new HttpException(500, "Partial submission is not allowed");
			}

			return null;
		}

		private AptifriedKnowledgeResultDto PostResult(AptifriedQuestionTreeSubmissionDto submissionDto, AptifriedKnowledgeResultController resultController) {
			AptifriedKnowledgeResultDto result = resultController.Post(new AptifriedKnowledgeResultDto() {
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now,
				IsComplete = submissionDto.IsComplete,
				KnowledgeCaptureMode = new AptifriedKnowledgeCaptureModeDto() {
					Id = submissionDto.KnowledgeCaptureModeId
				},
				Person = new AptifriedPersonDto() {
					Id = submissionDto.PersonId
				},
				QuestionTreeId = submissionDto.QuestionTreeId
			});

			if (result != null) {
				return result;
			} else {
				return null;
			}
		}

		private AptifriedKnowledgeResultDetailDto PostResultDetail(AptifriedKnowledgeResultDetailController resultDetailController, AptifriedQuestionTreeSubmissionDto submissionDto, int sequence, AptifriedKnowledgeResultDto resultObject) {
			AptifriedQuestionDto question = submissionDto.Questions[sequence];
			string answerValue = submissionDto.Answers[sequence];
			int questionBranchId = submissionDto.QuestionBranchIds[sequence];
			
			// Handle if we're answering from among a set list of predetermined answers (e.g., a combo box or radio list)
			int answerValueInt;
			AptifriedKnowledgeAnswerDto knowledgeAnswerDto = new AptifriedKnowledgeAnswerDto() {
				Id = USER_ENTERED_KNOWLEDGE_ANSWER_ID
			};

			if (Int32.TryParse(answerValue, out answerValueInt)) {
				AptifriedQuestionKnowledgeAnswerDto knowledgeAnswer = question.QuestionKnowledgeAnswers.Where(ka => ka.KnowledgeAnswer.Id == answerValueInt).FirstOrDefault();
				if (knowledgeAnswer != null) {
					knowledgeAnswerDto = new AptifriedKnowledgeAnswerDto() {
						Id = knowledgeAnswer.KnowledgeAnswer.Id
					};
				}
			}

			AptifriedKnowledgeResultDetailDto result = resultDetailController.Post(new AptifriedKnowledgeResultDetailDto() {
				KnowledgeResult = new AptifriedKnowledgeResultDto() {
					Id = resultObject.Id
				},
				Sequence = sequence + 1,
				QuestionTree = new AptifriedQuestionTreeDto() {
					Id = submissionDto.QuestionTreeId
				},
				QuestionBranch = new AptifriedQuestionBranchDto() {
					Id = questionBranchId
				},
				Question = new AptifriedQuestionDto() {
					Id = question.Id
				},
				KnowledgeAnswer = knowledgeAnswerDto,
				KnowledgeAnswerValue = answerValue,
				HtmlName = answerValue
			});

			if (result != null) {
				return result;
			} else {
				return null;
			}
		}
	}
}