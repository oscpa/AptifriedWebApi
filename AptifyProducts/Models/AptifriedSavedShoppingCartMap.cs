using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedSavedShoppingCartMap : ClassMap<AptifriedSavedShoppingCart> {
        public AptifriedSavedShoppingCartMap() {
            Table("vwWebShoppingCarts");

            Id(x => x.Id);
            Map(x => x.Name);
            HasOne(x => x.WebUser).ForeignKey("WebUserID");
            Map(x => x.XmlData);
            Map(x => x.Description);
            Map(x => x.OrderId);
            Map(x => x.DateCreated);
            Map(x => x.DateUpdated);
            References(x => x.Type).Column("WebShoppingCartTypeID");

            ReadOnly();
        }
    }
}