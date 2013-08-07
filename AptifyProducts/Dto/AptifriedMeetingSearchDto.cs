#region using

using System;
using System.Collections.Generic;
using System.Linq;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models;
using FluentNHibernate.Conventions;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingSearchDto
    {
            public IList<AptifriedMeetingTypeItemDto> MeetingTypes { get; set; }
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
                get { return !Zip.IsNull() && !MilesDistance.IsNull() && MilesDistance > 0; }
            }

            public bool HasLevels
            {
                get { return Levels != null && Levels.Any(); }
            }

        public bool HasMeetingTypeItems
        {
            get
            {
                return MeetingTypes.IsNotNull();
            }
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