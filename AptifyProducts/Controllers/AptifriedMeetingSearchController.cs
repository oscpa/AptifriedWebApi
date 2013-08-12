
 #region using

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
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

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public AptifriedMeetingSearchDto Get()
        {
            var msDto = new AptifriedMeetingSearchDto
            {
                MeetingTypes = session.GetActiveMeetingTypeItemsDto()
            };

            return msDto;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        public List<AptifriedMeetingTDto> Post(AptifriedMeetingSearchDto search)
        {
            if (search.IsNull())
                throw new HttpException(500, "Post must contain a search object", new ArgumentException("search"));

            var res = new SearchRepository<AptifriedMeetingT, AptifriedMeetingSearchDto>(session).Search(search, search.IsKeywordSearch);

            var results = Mapper.Map(res, new List<AptifriedMeetingTDto>());

            return results;
            //GroupBy done here because automapper doesn't support IEnum<IGroup<...
            //return results.GroupBy(x => x.ParentId);
        }
    }
}