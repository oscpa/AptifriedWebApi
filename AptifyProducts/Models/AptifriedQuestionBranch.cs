using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionBranch {
		public virtual int Id { get; set; }
		public virtual int QuestionTreeId { get; set; }
		public virtual AptifriedQuestion Question { get; set; }
		public virtual AptifriedQuestionBranch NextQuestionBranch { get; set; }
		public virtual bool CheckAnswers { get; set; }
		public virtual bool PageBreak { get; set; }
		public virtual bool ForceSave { get; set; }
		public virtual bool AnswerRequired { get; set; }
	}
}