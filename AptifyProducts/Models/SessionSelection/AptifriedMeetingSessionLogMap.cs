using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models.SessionSelection {
    public class AptifriedMeetingSessionLogMap : ClassMap<AptifriedMeetingSessionLog> {
        public AptifriedMeetingSessionLogMap() {
            Table("vwOSCPAMeetingSessionLogs");
            Id(x => x.Id);
            Map(x => x.BillToId);
            Map(x => x.BillToCompanyId);
            Map(x => x.ShipToId);
            Map(x => x.ShipToCompanyId);


            HasMany(x => x.NewSessions)
                .Table("vwOSCPANewMeetingSessions")
                .KeyColumns.Add("OSCPAMeetingSessionLogID");
            HasMany(x => x.CancelledSessions)
                .Table("vwOSCPACancelledMeetingSessions")
                .KeyColumns.Add("OSCPAMeetingSessionLogID");

            ReadOnly();
        }
    }
}