using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedKnowledgeResultController : AptifyEnabledApiController {

		public AptifriedKnowledgeResultController(ISession session) : base(session) { }

		public AptifriedKnowledgeResultDto Post(AptifriedKnowledgeResultDto result) {
			// Get the question tree corresponding to the request
			AptifriedQuestionTreeController questionTreeController = new AptifriedQuestionTreeController(session);
			if (questionTreeController == null) {
				throw new HttpException(500, "Couldn't instantiate a QuestionTreeController for this session!");
			}

			AptifriedQuestionTree questionTree = questionTreeController.GetById(result.QuestionTreeId).FirstOrDefault();
			if (questionTree == null) {
				throw new HttpException(500, "Couldn't find a question tree for that ID");
			}

			// Find any existing results for this person-tree pair
			IList<AptifriedKnowledgeResult> existingResults =
				session.QueryOver<AptifriedKnowledgeResult>()
				.Where(x =>
					x.Person.Id == result.Person.Id
					&& x.QuestionTreeId == result.QuestionTreeId)
				.List<AptifriedKnowledgeResult>();

			// Check conditions
			if (!questionTree.AllowDuplicates && existingResults.Count() > 0) {
				throw new HttpException(500, "Question tree disallows duplicates and results already exist for this person and tree");
			}

			if (!questionTree.AllowPartialCompletion && !result.IsComplete) {
				throw new HttpException(500, "Question tree requires completion but result is not complete");
			}

			if (questionTree.EndDate < DateTime.Now) {
				throw new HttpException(500, "Question tree end date is in the past");
			}

			if (questionTree.StartDate > DateTime.Now) {
				throw new HttpException(500, "Question tree start date is in the future");
			}

			// Persist values
			AptifyGenericEntityBase geKnowledgeResult = AptifyApp.GetEntityObject("Knowledge Results", -1);
			if (geKnowledgeResult == null) {
				throw new HttpException(500, "Received a null object when trying to instantiate a new Aptify GE");
			}

			geKnowledgeResult.SetValue("QuestionTreeID", result.QuestionTreeId);
			geKnowledgeResult.SetValue("DateCreated", result.DateCreated);
			geKnowledgeResult.SetValue("DateUpdated", result.DateUpdated);
			geKnowledgeResult.SetValue("KnowledgeCaptureModeID", result.KnowledgeCaptureMode.Id);
			geKnowledgeResult.SetValue("PersonID", result.Person.Id);
			geKnowledgeResult.SetValue("IsComplete", result.IsComplete);

			if (!geKnowledgeResult.Save(false)) {
				throw new HttpException(500, geKnowledgeResult.LastUserError);
			}

			result.Id = Convert.ToInt32(geKnowledgeResult.RecordID);

			return result;
		}

	}
}