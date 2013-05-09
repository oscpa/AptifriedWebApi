using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedProductRelationDto {
		public int Id { get; set; }
		public AptifriedProductDto Product { get; set; }
		public int Sequence { get; set; }
		public AptifriedProductDto RelatedProduct { get; set; }
		public AptifriedProductRelationshipTypeDto ProductRelationshipType { get; set; }
		public bool IsActive { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool AutoPrompt { get; set; }
		public string PromptText { get; set; }
		public bool WebPrompt { get; set; }
		public string WebPromptText { get; set; }
		public string Comments { get; set; }
	}
}