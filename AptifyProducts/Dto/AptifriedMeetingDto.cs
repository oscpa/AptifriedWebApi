using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingDto {
        public int Id { get; set; }
        public string MeetingTitle { get; set; }
        public AptifriedProductDto Product { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public AptifriedMeetingStatusDto Status { get; set; }
		public AptifriedMeetingTypeDto Type { get; set; }
		public TimeSpan OpenTime { get; set; }
        public AptifriedAddressDto Location { get; set; }
		public IList<AptifriedMeetingEductionUnitsDto> Credits { get; set; }
		public int MaxRegistrants { get; set; }
    }
}