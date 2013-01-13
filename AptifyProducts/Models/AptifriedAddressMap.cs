using AptifyWebApi.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAddressMap : ClassMap<AptifriedAddress> {

        public AptifriedAddressMap() {
            Table("vwAddressesTiny");
            Id(x => x.Id);
            Map(x => x.Line1);
            Map(x => x.Line2);
            Map(x => x.Line3);
            Map(x => x.City);
            Map(x => x.StateProvince);
            Map(x => x.PostalCode);
        }
    }
}