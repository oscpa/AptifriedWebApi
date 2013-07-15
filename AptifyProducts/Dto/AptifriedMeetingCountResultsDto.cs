using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Models.Dto.Meeting;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingCountResultsDto {
        public AptifriedMeetingSearchDto SearchEntered { get; set; }
        public int Count { get; set; }
    }
}