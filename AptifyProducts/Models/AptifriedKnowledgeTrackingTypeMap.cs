using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeTrackingTypeMap : ClassMap<AptifriedKnowledgeTrackingType> {
		public AptifriedKnowledgeTrackingTypeMap() {
			Table("vwKnowledgeTrackingTypes");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.Description);
		}
	}
}