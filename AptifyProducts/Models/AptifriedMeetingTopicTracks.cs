using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTopicTracks {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<AptifriedMeetingTopicTracksProduct> TopicTrackProduct { get; set; }
    }
}