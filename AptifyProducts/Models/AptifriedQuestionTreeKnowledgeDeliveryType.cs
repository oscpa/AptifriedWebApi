using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionTreeKnowledgeDeliveryType {
		public virtual int Id { get; set; }
		public virtual int QuestionTreeId { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedKnowledgeDeliveryType KnowledgeDeliveryType { get; set; }
		public virtual string AccessPassword { get; set; }
	}
}