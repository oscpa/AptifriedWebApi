using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedProductRelation {
		public virtual int Id { get; set; }
		public virtual AptifriedProduct Product { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedProduct RelatedProduct { get; set; }
		public virtual AptifriedProductRelationshipType ProductRelationshipType { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual bool AutoPrompt { get; set; }
		public virtual string PromptText { get; set; }
		public virtual bool WebPrompt { get; set; }
		public virtual string WebPromptText { get; set; }
		public virtual string Comments { get; set; }
	}
}