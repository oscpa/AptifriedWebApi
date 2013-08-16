#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedWebShoppingCartDetailsMap : ClassMap<AptifriedWebShoppingCartDetails>
    {
        public AptifriedWebShoppingCartDetailsMap()
        {
            Table("vwWebShoppingCartDetails");
            Id(x => x.Id);
            Map(x => x.WebShoppingCartId);
            Map(x => x.ProductId);
            Map(x => x.RegistrantId);
            References(x => x.Campaign).Column("CampaignID");
            ReadOnly();
        }
    }
}