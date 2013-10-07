using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionBranchAnswerBranch {
		public virtual int Id { get; set; }
		public virtual AptifriedQuestionBranch QuestionBranch { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedKnowledgeAnswer KnowledgeAnswer { get; set; }
		public virtual AptifriedQuestionBranch NextQuestionBranch { get; set; }
	}
}