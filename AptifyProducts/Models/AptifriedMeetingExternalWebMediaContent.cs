using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingExternalWebMediaContent {
        public virtual int Id { get; set; }
        public virtual int MeetingId { get; set; }
        public virtual AptifriedMeetingWebMediaType MediaType { get; set; }
        public virtual string MediaFilePath { get; set; }
        public virtual string IFrameCode { get; set; }
        public virtual string MediaImagePath { get; set; }
        public virtual bool RequireMeetingRegistration { get; set; }
        public virtual string VideoId { get; set; }

    }
}