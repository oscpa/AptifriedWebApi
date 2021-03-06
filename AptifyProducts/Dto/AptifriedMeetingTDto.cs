﻿#region using

using System;
using System.Collections.Generic;
using AptifyWebApi.Models;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingTDto
    {
        public int Id { get; set; }
        public string MeetingTitle { get; set; }
        public AptifriedProductDto Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public AptifriedMeetingTypeItemDto TypeItem { get; set; }
        public TimeSpan OpenTime { get; set; }
        public AptifriedAddressDto Location { get; set; }
        public IList<AptifriedMeetingEductionUnitsDto> Credits { get; set; }
        public int MaxRegistrants { get; set; }
        public AptifriedVenueDto Venue { get; set; }
        public int? ParentId { get; set; }

        public int Relevance { get; set; }
        //public int Miles { get; set; }
        /// <summary>
        /// Couldn't strongly type this column becuase we don't enforce referential integrity on it.
        /// </summary>
        public int ClassLevelId { get; set; }
    }
}