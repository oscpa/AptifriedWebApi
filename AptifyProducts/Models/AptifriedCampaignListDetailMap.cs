using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedCampaignListDetailMap : ClassMap<AptifriedCampaignListDetail> {
		public AptifriedCampaignListDetailMap() {
			Table("vwCampaignListDetailWebServices");
			Id(x => x.Id);
			Map(x => x.CampaignId);
			Map(x => x.PersonId);
		}
	}
}