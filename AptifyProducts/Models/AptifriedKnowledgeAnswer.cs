using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeAnswer {
		public virtual int Id { get; set; }
		public virtual string Text { get; set; }
		public virtual AptifriedKnowledgeCategory KnowledgeCategory { get; set; }
		public virtual string Description { get; set; }
	}
}