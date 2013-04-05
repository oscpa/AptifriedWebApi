using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingExternalWebMediaContentDto {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public AptifriedMeetingWebMediaTypeDto MediaType { get; set; }
        public string MediaFilePath { get; set; }
        public string IFrameCode { get; set; }
        public string MediaImagePath { get; set; }
        public bool RequireMeetingRegistration { get; set; }
        public string VideoId { get; set; }
    }
}