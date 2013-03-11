using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedLicenseStatusMap : ClassMap<AptifriedLicenseStatus> {
		public AptifriedLicenseStatusMap() {
			Table("vwLicenseStatus");
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.OldId);
            ReadOnly();
		}
	}
}