#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTopicTracksProductMap : ClassMap<AptifriedMeetingTopicTrackProduct>
    {
        public AptifriedMeetingTopicTracksProductMap()
        {
            Table("vwOSCPAMeetingTopicTrackProducts");
            Id(x => x.Id);
            Map(x => x.Sequence);
            References(x => x.Product).Column("ProductID");
        }
    }
}