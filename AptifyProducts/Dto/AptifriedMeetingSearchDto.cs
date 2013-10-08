#region using

using System;
using System.Collections.Generic;
using System.Linq;
using AptifyWebApi.Helpers;
using NHibernate.Linq;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingSearchDto
    {
        public IList<AptifriedMeetingTypeItemDto> MeetingTypes { get; set; }
        public IList<int> Levels { get; set; }
        public string SearchText { get; set; }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value.IsNotNull() ? value.As<DateTime>().Add(new TimeSpan(0,0,0)) : value; }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value.IsNotNull() ? value.As<DateTime>().Add(new TimeSpan(23, 59, 59)) : value; }
        }

        public IList<AptifriedMeetingEductionUnitsDto> CreditTypes { get; set; }

        public int MilesDistance { get; set; }
        public string Zip { get; set; }
        public string UserId { get; set; }

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
            get { return !string.IsNullOrWhiteSpace(Zip) && !MilesDistance.IsNull() && MilesDistance > 0; }
        }

        public bool HasLevels
        {
            get { return Levels != null && Levels.Any(); }
        }

        public bool HasMeetingTypeItems
        {
            get { return MeetingTypes.IsNotNull(); }
        }

        public bool HasMeetingTypes
        {
            //initialized && has types
            get { return HasMeetingTypeItems && MeetingTypes.Any(x => x.Type.IsNotNull()); }
        }

        public bool HasTypeGroups
        {
            get { return HasMeetingTypeItems && MeetingTypes.Any(x => x.Group.IsNotNull()); }
        }
    }
}