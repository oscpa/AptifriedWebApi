using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingTopicTrackProductDto {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public AptifriedProductDto Product { get; set; }
    }
}