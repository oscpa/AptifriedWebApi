using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeDeliveryTypeMap : ClassMap<AptifriedKnowledgeDeliveryType> {
		public AptifriedKnowledgeDeliveryTypeMap() {
			Table("vwKnowledgeDeliveryTypes");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.Description);
		}
	}
}