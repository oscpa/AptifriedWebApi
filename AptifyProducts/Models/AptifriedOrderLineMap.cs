#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedOrderLineMap : ClassMap<AptifriedOrderLine>
    {
        public AptifriedOrderLineMap()
        {
            Table("vwOrderDetails");
            Id(x => x.Id);
            Map(x => x.OrderId);
            Map(x => x.Price);
            Map(x => x.Discount);
            References(x => x.Product).Column("ProductID");
            References(x => x.Campaign).Column("CampaignCodeID");

            ReadOnly();
        }
    }
}