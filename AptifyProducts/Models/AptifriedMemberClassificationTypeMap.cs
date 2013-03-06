using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedMemberClassificationTypeMap : ClassMap<AptifriedMemberClassificationType> {
		public AptifriedMemberClassificationTypeMap() {
			Table("vwMemberClassificationTypes");
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Description);
			Map(x => x.IsActive);
			Map(x => x.DefaultType);
			Map(x => x.OldID);
		}
	}
}