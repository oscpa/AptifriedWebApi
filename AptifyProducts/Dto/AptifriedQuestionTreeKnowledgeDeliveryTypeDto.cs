using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionTreeKnowledgeDeliveryTypeDto {
		public int Id { get; set; }
		public int QuestionTreeId { get; set; }
		public int Sequence { get; set; }
		public AptifriedKnowledgeDeliveryTypeDto KnowledgeDeliveryType { get; set; }
		public string AccessPassword { get; set; }
	}
}