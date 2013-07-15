#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTimeSpanProductMap : ClassMap<AptifriedMeetingTimeSpanProduct>
    {
        public AptifriedMeetingTimeSpanProductMap()
        {
            Table("vwOSCPAMeetingTimeSpanProducts");
            Id(x => x.Id);
            Map(x => x.Sequence);
            References(x => x.Product).Column("ProductID");
        }
    }
}