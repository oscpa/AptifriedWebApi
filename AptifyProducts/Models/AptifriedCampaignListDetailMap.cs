#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedCampaignListDetailMap : ClassMap<AptifriedCampaignListDetail>
    {
        public AptifriedCampaignListDetailMap()
        {
            Table("vwCampaignListDetailWebServices");
            Id(x => x.Id);
            Map(x => x.CampaignId);
            Map(x => x.PersonId);
        }
    }
}