using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTopicTrack {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual int MeetingID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<AptifriedMeetingTopicTrackProduct> TopicTrackProduct { get; set; }
    }
}