#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedCampaign
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual IList<AptifriedCampaignListDetail> CampaignListDetail { get; set; }
    }
}