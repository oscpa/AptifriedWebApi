using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedCampaignMap : ClassMap<AptifriedCampaign> {
		public AptifriedCampaignMap() {
			Table("vwCampaigns");
			Id(x => x.Id);
			Map(x => x.Code);
			Map(x => x.Status);
		}
	}
}