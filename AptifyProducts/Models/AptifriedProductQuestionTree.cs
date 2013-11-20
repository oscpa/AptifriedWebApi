using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedProductQuestionTree {
		public virtual int Id { get; set; }
		public virtual int Sequence { get; set; }
		public virtual int ProductId { get; set; }
		public virtual int QuestionTreeId { get; set; }
	}
}