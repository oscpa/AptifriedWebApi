using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedKnowledgeResultDto {
		public int Id { get; set; }
		public int QuestionTreeId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public AptifriedKnowledgeCaptureModeDto KnowledgeCaptureMode { get; set; }
		public AptifriedPersonDto Person { get; set; }
		public bool IsComplete { get; set; }
	}
}