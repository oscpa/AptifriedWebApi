using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionKnowledgeAnswer {
		public virtual int Id { get; set; }
		public virtual AptifriedQuestion Question { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedKnowledgeAnswer KnowledgeAnswer { get; set; }
		public virtual bool AnswerRequired { get; set; }

	}
}