using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedCampaignDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public IList<AptifriedCampaignListDetailDto> CampaignListDetail { get; set; }
	}
}