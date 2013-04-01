using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedProductTypeMap : ClassMap<AptifriedProductType>{
        public AptifriedProductTypeMap() {
            Table("vwProductTypes");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}