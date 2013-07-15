#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTopicTrack
    {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual int MeetingId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<AptifriedMeetingTopicTrackProduct> TopicTrackProduct { get; set; }
    }
}