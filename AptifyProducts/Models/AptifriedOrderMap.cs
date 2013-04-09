using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedOrderMap : ClassMap<AptifriedOrder> {
        public AptifriedOrderMap() {
            Table("vwOrders");
            Id(x => x.Id);
            Map(x => x.OrderDate);
            Map(x => x.Balance).Column("Balance");
            Map(x => x.GrandTotal).Column("CALC_GrandTotal");
            Map(x => x.ShippingTotal).Column("CALC_AdjustedTotal");
            Map(x => x.SubTotal).Column("CALC_SubTotal");
            Map(x => x.Tax).Column("CALC_SalesTax");
            References(x => x.ShipToPerson).Column("ShipToID");
            References(x => x.ShippingAddress).Column("ShipToAddressID");
            HasMany(x => x.Lines)
                .Table("vwOrderLines")
                .KeyColumn("OrderID");
            ReadOnly();
        }
    }
}