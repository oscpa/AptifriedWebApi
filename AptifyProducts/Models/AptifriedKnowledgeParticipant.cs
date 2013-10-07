using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeParticipant {
		public virtual int Id { get; set; }
		public virtual AptifriedPerson Person { get; set; }
		public virtual int QuestionTreeId { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual DateTime DateUpdated { get; set; }
		public virtual AptifriedKnowledgeResult KnowledgeResult { get; set; }
		public virtual bool IsComplete { get; set; }
	}
}