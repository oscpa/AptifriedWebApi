#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedCampaignMap : ClassMap<AptifriedCampaign>
    {
        public AptifriedCampaignMap()
        {
            Table("vwCampaignsWebServices");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Code);
            HasMany(x => x.CampaignListDetail).KeyColumn("CampaignID");
        }
    }
}