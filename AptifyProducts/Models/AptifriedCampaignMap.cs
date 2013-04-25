using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedCampaignMap : ClassMap<AptifriedCampaign> {
		public AptifriedCampaignMap() {
			Table("vwCampaignsWebServices");
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Code);
			HasMany(x => x.CampaignListDetail).KeyColumn("CampaignID");
		}
	}
}