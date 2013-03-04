using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebShoppingCartTypeMap : ClassMap<AptifriedWebShoppingCartType> {
        public AptifriedWebShoppingCartTypeMap() {
            Table("vwWebShoppingCartTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}