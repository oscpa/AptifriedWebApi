using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTopicTrackMap : ClassMap<AptifriedMeetingTopicTrack> {
        public AptifriedMeetingTopicTrackMap() {
            Table("vwOSCPAMeetingTopicTracks");
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.Name);
            Map(x => x.MeetingID);
            HasMany(x => x.TopicTrackProduct)
                .Table("vwOSCPAMeetingTopicTrackProducts")
                .KeyColumns.Add("OSCPAMeetingTopicTrackID");
        }
    }
}