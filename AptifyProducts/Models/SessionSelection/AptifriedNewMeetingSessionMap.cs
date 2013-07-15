#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.SessionSelection
{
    public class AptifriedNewMeetingSessionMap : ClassMap<AptifriedNewMeetingSession>
    {
        public AptifriedNewMeetingSessionMap()
        {
            Table("vwOSCPANewMeetingSessions");
            Id(x => x.Id);
            Map(x => x.OscpaMeetingSessionLogId);
            Map(x => x.ProductId);
            Map(x => x.AttendeeStatusId);

            ReadOnly();
        }
    }
}