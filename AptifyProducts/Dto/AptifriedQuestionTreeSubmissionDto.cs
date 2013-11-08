using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionTreeSubmissionDto {
		public int PersonId { get; set; }
		public int QuestionTreeId { get; set; }
		public IList<AptifriedQuestionDto> Questions { get; set; }
		public IList<string> Answers { get; set; }
		public IList<int> QuestionBranchIds { get; set; }
		public IList<int> KnowledgeAnswerIds { get; set; }
		public bool IsComplete { get; set; }
		public int KnowledgeCaptureModeId { get; set; }
	}
}