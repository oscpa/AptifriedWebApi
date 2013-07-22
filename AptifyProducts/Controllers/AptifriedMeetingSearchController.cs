
 #region using

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using AptifyWebApi.Repository;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingSearchController : AptifyEnabledApiController
    {
        public AptifriedMeetingSearchController(ISession session) : base(session)
        {
        }

        [HttpGet]
        public AptifriedMeetingSearchDto Get()
        {
            var msDto = new AptifriedMeetingSearchDto
            {
                MeetingTypes = session.GetAllMeetingTypeDto()
            };

            return msDto;
        }

        [HttpPost]
        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSearchDto search)
        {
#if DEBUG
            var list = new List<string>
                {
                    "Standard",
                    "Webcast",
                    "Conference",
                    "On-Site",
                    "Self-Study",
                    "Session",
                    "Webinar",
                    "Seminar",
                    "Networking",
                    "Other"
                };
#endif


            // If search is null, throw an error
            if (search.IsNull())
                throw new HttpException(500, "Post must contain a search object", new ArgumentException("search"));

            var res = new SearchRepository<AptifriedMeeting, AptifriedMeetingSearchDto>(session).Search(search, false);

            var results = Mapper.Map(res, new List<AptifriedMeetingDto>());

            return results;
        }
    }
}