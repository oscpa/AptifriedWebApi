
 #region using

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
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
            // If search is null, throw an error
            if (search.IsNull())
                throw new HttpException(500, "Post must contain a search object", new ArgumentException("search"));

            var list = new List<string>();
            list.Add("Standard");
            list.Add("Webcast");
            list.Add("Conference");
            list.Add("On-Site");
            list.Add("Self-Study");
            list.Add("Session");
            list.Add("Webinar");
            list.Add("Seminar");
            list.Add("Networking");
            list.Add("Other");
            
            
            var res = new SearchRepository(session).Search(search, true);

            var results = Mapper.Map(res, new List<AptifriedMeetingDto>());

            return results;
        }
    }
}