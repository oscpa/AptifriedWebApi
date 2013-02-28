using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPaymentTypeMap : ClassMap<AptifriedPaymentType> {
        public AptifriedPaymentTypeMap() {
            Table("vwPaymentTypes");
            Id(x => x.Id);
            Map(x => x.Active);
            Map(x => x.Inflow);
            Map(x => x.Name);
            Map(x => x.Type);
        }
    }
}