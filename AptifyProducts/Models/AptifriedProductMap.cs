using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedProductMap : ClassMap<AptifriedProduct> {
        public AptifriedProductMap() {
            Table("vwProductsTiny");
            Id(x => x.Id);
            /*Map(x => x.Name);*/
            Map(x => x.Code);
            HasMany(x => x.Prices)
                .Table("vwProductPrices")
                .KeyColumns.Add("ProductID");
        }
    }
}