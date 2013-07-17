#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedWebShoppingCartMap : ClassMap<AptifriedWebShoppingCart>
    {
        public AptifriedWebShoppingCartMap()
        {
            Table("vwWebShoppingCarts");

            Id(x => x.Id);
            Map(x => x.Name);
            HasOne(x => x.WebUser).ForeignKey("WebUserID");
            Map(x => x.Description);
            Map(x => x.OrderId);
            Map(x => x.DateCreated);
            Map(x => x.DateUpdated);
            References(x => x.Type).Column("WebShoppingCartTypeID");
            HasMany(x => x.Lines).Table("vwWebShoppingCartDetails").KeyColumn("WebShoppingCartID");
            ReadOnly();
        }
    }
}