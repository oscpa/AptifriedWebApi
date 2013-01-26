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
            Map(x => x.WebUserId);
            Map(x => x.XmlData);
            Map(x => x.Description);
            Map(x => x.OrderId);
        }
    }
}