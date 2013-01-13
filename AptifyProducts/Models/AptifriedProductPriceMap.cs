
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedProductPriceMap : ClassMap<AptifriedProductPrice> {
        public AptifriedProductPriceMap() {
            Table("vwProductPrices");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ProductId);
            Map(x => x.Price);
            References(x => x.MemberType)
                .Column("MemberTypeID");
        }
    }
}