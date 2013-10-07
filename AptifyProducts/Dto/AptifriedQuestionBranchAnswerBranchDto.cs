using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionBranchAnswerBranchDto {
		public int Id { get; set; }
		public AptifriedQuestionBranchDto QuestionBranch { get; set; }
		public int Sequence { get; set; }
		public AptifriedKnowledgeAnswerDto KnowledgeAnswer { get; set; }
		public AptifriedQuestionBranchDto NextQuestionBranch { get; set; }
	}
}