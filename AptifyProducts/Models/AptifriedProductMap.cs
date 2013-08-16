#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedProductMap : ClassMap<AptifriedProduct>
    {
        public AptifriedProductMap()
        {
            Table("vwProductsTinyWebServices");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Code);
            HasMany(x => x.Prices)
                .Table("vwProductPrices")
                .KeyColumns.Add("ProductID");

            HasMany(x => x.Objectives)
                .Table("vwOSCPAMarketingObjectives")
                .KeyColumns.Add("ProductID");

            Map(x => x.AdditionalInformation).Column("OscpaAdditionalInformation");
            Map(x => x.WhoShouldPurchase).Column("OSCPAWhoShouldPurchase");
            Map(x => x.Summary).Column("OSCPASummary");

            Map(x => x.IsSold);
            Map(x => x.WebEnabled);

            References(x => x.Type).Column("ProductTypeID");
        }
    }
}