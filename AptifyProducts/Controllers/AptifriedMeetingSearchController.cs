#region using

using System;
using System.Collections.Generic;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Dto.Meeting;
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

        public AptifriedMeetingSearchDto Get()
        {
            return new AptifriedMeetingSearchDto
                {
                    MeetingTypes = session.GetInitMeetingTypeDto()
                };
        }

        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSearchDto search)
        {
            // If search is null, throw an error
            if (search.IsNull())
                throw new HttpException(500, "Post must contain a search object", new ArgumentException("search"));

            var res = new SearchRepository(session).Search(search, true);

            var results = Mapper.Map(res, new List<AptifriedMeetingDto>());

            return results;
        }
    }
}