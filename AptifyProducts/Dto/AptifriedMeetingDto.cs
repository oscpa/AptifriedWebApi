using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingDto {
        public int Id { get; set; }
        public string MeetingTitle { get; set; }
        public int ProductId { get; set; }
        public string MeetingTypeName { get; set; }
        public string MeetingStatusName { get; set; }
        public AptifriedAddressDto Location { get; set; }
        public AptifriedProductDto Product { get; set; }
        public IList<AptifriedMeetingEductionUnitsDto> Credits { get; set; }

    }
}