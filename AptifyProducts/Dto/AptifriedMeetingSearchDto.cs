using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMeetingSearchDto {
        public IList<string> MeetingType { get; set; }
        public IList<int> Levels { get; set; }
        public string SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<AptifriedMeetingEductionUnitsDto> CreditTypes { get; set; }
        public int MilesDistance { get; set; }
        public string Zip { get; set; }
        
    }
}