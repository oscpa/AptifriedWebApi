#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTopicTrackMap : ClassMap<AptifriedMeetingTopicTrack>
    {
        public AptifriedMeetingTopicTrackMap()
        {
            Table("vwOSCPAMeetingTopicTracks");
            Id(x => x.Id);
            Map(x => x.Sequence);
            Map(x => x.Name);
            Map(x => x.MeetingId);
            HasMany(x => x.TopicTrackProduct)
                .Table("vwOSCPAMeetingTopicTrackProducts")
                .KeyColumns.Add("OSCPAMeetingTopicTrackID");
        }
    }
}