#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.SessionSelection
{
    public class AptifriedCancelledMeetingSessionsMap : ClassMap<AptifriedCancelledMeetingSessions>
    {
        public AptifriedCancelledMeetingSessionsMap()
        {
            Table("vwOSCPACancelledMeetingSessions");
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.OrderMeetingDetailId);
            Map(x => x.OrderId);
            Map(x => x.ProductId);
            Map(x => x.StatusId);
            ReadOnly();
        }
    }
}