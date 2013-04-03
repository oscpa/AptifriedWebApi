using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models.SessionSelection {
    public class AptifriedNewMeetingSessionMap : ClassMap<AptifriedNewMeetingSession> {
        public AptifriedNewMeetingSessionMap() {
            Table("vwOSCPANewMeetingSessions");
            Id(x => x.Id);
            Map(x => x.OscpaMeetingSessionLogId);
            Map(x => x.ProductId);
            Map(x => x.AttendeeStatusId);

            ReadOnly();
        }
    }
}