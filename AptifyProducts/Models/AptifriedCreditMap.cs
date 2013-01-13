using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedCreditMap : ClassMap<AptifriedCredit> {
        public AptifriedCreditMap() {
            Table("vwClassCPECredits");
            Id(m => m.Id);
            Map(x => x.Name).Column("CPETypeIDName");
            Map(x => x.Code).Column("CPECodeID_Code");
            Map(x => x.Amount).Column("Credits");


        }
    }
}