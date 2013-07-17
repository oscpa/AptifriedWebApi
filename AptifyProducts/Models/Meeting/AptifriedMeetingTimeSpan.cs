#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTimeSpan
    {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual int MeetingId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual IList<AptifriedMeetingTimeSpanProduct> TimeSpanProducts { get; set; }
    }
}