using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTopicTracksProductMap: ClassMap<AptifriedMeetingTopicTracksProduct> {
        public AptifriedMeetingTopicTracksProductMap() {
            Table("vwOSCPAMeetingTopicTrackProducts");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Sequence);
            References(x => x.Product).Column("ProductID");
        }
    }
}