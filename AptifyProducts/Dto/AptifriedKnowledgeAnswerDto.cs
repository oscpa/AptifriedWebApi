using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedKnowledgeAnswerDto {
		public int Id { get; set; }
		public string Text { get; set; }
		public AptifriedKnowledgeCategoryDto KnowledgeCategory { get; set; }
		public string Description { get; set; }
	}
}