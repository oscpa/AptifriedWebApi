#region using

using System;
using System.Collections.Generic;
using System.Linq;
using AptifyWebApi.Helpers;

#endregion

namespace AptifyWebApi.Models.Dto.Meeting
{
    public class AptifriedMeetingSearchDto
    {
        public IList<AptifriedMeetingTypeDto> MeetingTypes { get; set; }
        public IList<int> Levels { get; set; }
        public string SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<AptifriedMeetingEductionUnitsDto> CreditTypes { get; set; }
        public int MilesDistance { get; set; }
        public string Zip { get; set; }

        public bool IsDateSearch
        {
            get { return StartDate.HasValue | EndDate.HasValue; }
        }

        public bool HasCreditTypes
        {
            get { return CreditTypes != null && CreditTypes.Any(); }
        }

        public bool IsKeywordSearch
        {
            get { return !string.IsNullOrWhiteSpace(SearchText); }
        }

        public bool IsZipSearch
        {
            get { return !Zip.IsNull() && MilesDistance > 0; }
        }

        public bool HasLevels
        {
            get { return Levels != null && Levels.Any(); }
        }

        public bool HasMeetingTypes
        {
            //initialized && has types
            get { return !MeetingTypes.IsNull() && MeetingTypes.Any(); }
        }
    }
}