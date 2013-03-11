using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingStatusMap : ClassMap<AptifriedMeetingStatus> {
        public AptifriedMeetingStatusMap() {
            Table("vwMeetingStati");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}