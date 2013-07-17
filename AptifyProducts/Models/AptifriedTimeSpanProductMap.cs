using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedTimeSpanProductMap : ClassMap<AptifriedTimeSpanProduct> {
        public AptifriedTimeSpanProductMap() {
            Table("vwOSCPAMeetingTimeSpanProducts");
            Id(x => x.Id);
            References(x => x.Product).Column("ProductID");
        }
    }
}