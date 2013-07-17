#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingStatusMap : ClassMap<AptifriedMeetingStatus>
    {
        public AptifriedMeetingStatusMap()
        {
            Table("vwMeetingStati");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}