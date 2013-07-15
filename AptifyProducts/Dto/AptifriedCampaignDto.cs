#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Dto
{
    public class AptifriedCampaignDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IList<AptifriedCampaignListDetailDto> CampaignListDetail { get; set; }
    }
}