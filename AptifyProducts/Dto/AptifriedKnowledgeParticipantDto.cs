using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedKnowledgeParticipantDto {
		public int Id { get; set; }
		public AptifriedPersonDto Person { get; set; }
		public int QuestionTreeId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public AptifriedKnowledgeResultDto KnowledgeResult { get; set; }
		public bool IsComplete { get; set; }
	}
}