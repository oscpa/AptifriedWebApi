using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeResultDetail {
		public virtual int Id { get; set; }
		public virtual AptifriedKnowledgeResult KnowledgeResult { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedQuestionTree QuestionTree { get; set; }
		public virtual AptifriedQuestionBranch QuestionBranch { get; set; }
		public virtual AptifriedQuestion Question { get; set; }
		public virtual AptifriedKnowledgeAnswer KnowledgeAnswer { get; set; }
		public virtual string KnowledgeAnswerValue { get; set; }
		public virtual string HtmlName { get; set; }
	}
}