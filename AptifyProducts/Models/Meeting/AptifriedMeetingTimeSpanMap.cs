#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTimeSpanMap : ClassMap<AptifriedMeetingTimeSpan>
    {
        public AptifriedMeetingTimeSpanMap()
        {
            Table("vwOSCPAMeetingTimeSpan");
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.MeetingId);
            Map(x => x.Name);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            HasMany(x => x.TimeSpanProducts)
                .Table("vwOSCPAMeetingTimeSpanProducts")
                .KeyColumns.Add("OSCPAMeetingTimeSpanID");
        }
    }
}