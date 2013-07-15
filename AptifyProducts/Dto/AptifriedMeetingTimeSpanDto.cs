#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Dto.Meeting
{
    public class AptifriedMeetingTimeSpanDto
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public int MeetingId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual IList<AptifriedMeetingTimeSpanProductDto> TimeSpanProducts { get; set; }
    }
}