﻿#region using

using System;
using System.Collections.Generic;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMeetingT : IBaseModel
    {
        public virtual int Id { get; set; }
        public virtual string MeetingTitle { get; set; }
        public virtual AptifriedProduct Product { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int StatusId { get; set; }
        public virtual AptifriedMeetingTypeItem TypeItem { get; set; }
        public virtual TimeSpan OpenTime { get; set; }
        public virtual AptifriedAddress Location { get; set; }
        public virtual IList<AptifriedMeetingEductionUnits> Credits { get; set; }
        public virtual int MaxRegistrants { get; set; }
        public virtual AptifriedVenue Venue { get; set; }
        public virtual int? ParentId { get; set; }

        public virtual int Relevance { get; set; }
        //public virtual int Miles { get; set; }

        /// <summary>
        /// This value could not be strongly typed as there is no referential integrity between class levels and
        /// meetings, as there are invalid primary keys. worthless.
        /// </summary>
        public virtual int ClassLevelId { get; set; }
    }
}