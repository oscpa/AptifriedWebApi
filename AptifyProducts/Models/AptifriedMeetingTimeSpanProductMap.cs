using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTimeSpanProductMap : ClassMap<AptifriedMeetingTimeSpanProduct> {
        public AptifriedMeetingTimeSpanProductMap() {
            Table("vwOSCPAMeetingTimeSpanProducts");
            Id(x => x.Id);
            Map(x => x.Sequence);
            References(x => x.Product).Column("ProductID");
        }
    }
}