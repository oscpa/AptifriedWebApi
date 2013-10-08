using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedKnowledgeResultDetailDto {
		public int Id { get; set; }
		public AptifriedKnowledgeResultDto KnowledgeResult { get; set; }
		public int Sequence { get; set; }
		public AptifriedQuestionTreeDto QuestionTree { get; set; }
		public AptifriedQuestionBranchDto QuestionBranch { get; set; }
		public AptifriedQuestionDto Question { get; set; }
		public AptifriedKnowledgeAnswerDto KnowledgeAnswer { get; set; }
		public string KnowledgeAnswerValue { get; set; }
		public string HtmlName { get; set; }
	}
}