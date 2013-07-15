#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Dto.Meeting
{
    public class AptifriedMeetingTopicTrackDto
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public int MeetingId { get; set; }

        public IList<AptifriedMeetingTopicTrackProductDto> TopicTrackProduct { get; set; }
    }
}