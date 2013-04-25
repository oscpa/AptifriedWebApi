using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedCampaignListDetail {
		public virtual int Id { get; set; }
		public virtual int CampaignId { get; set; }
		public virtual int PersonId { get; set; }
	}
}