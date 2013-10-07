using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionTypeDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool MultipleAnswersToSave { get; set; }
		public string Xsl { get; set; }
		public string Description { get; set; }
	}
}