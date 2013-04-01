using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTopicTracksProduct {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string Name { get; set; }
        public virtual AptifriedProduct Product { get; set; }
    }
}
