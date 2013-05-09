using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedProductRelationMap : ClassMap<AptifriedProductRelation> {
		public AptifriedProductRelationMap() {
			Table("vwProductRelations1");
			Id(x => x.Id);
			References(x => x.Product).Column("ProductID");
			Map(x => x.Sequence);
			References(x => x.RelatedProduct).Column("RelatedProductID");
			References(x => x.ProductRelationshipType).Column("ProductRelationshipTypeID");
			Map(x => x.IsActive);
			Map(x => x.StartDate);
			Map(x => x.EndDate);
			Map(x => x.AutoPrompt);
			Map(x => x.PromptText);
			Map(x => x.WebPrompt);
			Map(x => x.WebPromptText);
			Map(x => x.Comments);
		}
	}
}