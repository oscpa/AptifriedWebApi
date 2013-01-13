using AptifyWebApi.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedCompanyMap : ClassMap<AptifriedCompany> {
        public AptifriedCompanyMap() {
            Table("vwCompaniesTiny");
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Address).Column("AddressID");
        }
    }
}