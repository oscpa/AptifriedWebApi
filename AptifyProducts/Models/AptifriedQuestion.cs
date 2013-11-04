using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestion {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Text { get; set; }
		public virtual AptifriedKnowledgeCategory KnowledgeCategory { get; set; }
		public virtual AptifriedQuestionType QuestionType { get; set; }
		public virtual string Description { get; set; }
		public virtual IEnumerable<AptifriedQuestionKnowledgeAnswer> QuestionKnowledgeAnswers { get; set; }
	}
}