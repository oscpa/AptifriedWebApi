
 #region using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using AptifyWebApi.Repository;
using NHibernate;
using NHibernate.Hql.Ast.ANTLR.Tree;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingSearchCountController : AptifyEnabledApiController
    {
        public AptifriedMeetingSearchCountController(ISession session)
            : base(session)
        {
        }



        [HttpPost]
        public AptifriedMeetingCountResultsDto Post(AptifriedMeetingSearchDto search)
        {
            var cnt = 0;

            if(search.HasMeetingTypeItems)
                foreach (var mType in search.MeetingTypes.Where(mType => mType.Group.IsNotNull() && mType.Group.Id.IsNotNull()))
                    cnt += new SearchRepository<AptifriedMeetingT, AptifriedMeetingSearchDto>(session).Search(search, false).Count();


            if (search.HasMeetingTypeItems)
                foreach (var mType in search.MeetingTypes.Where(mType => mType.Type.IsNotNull() && mType.Type.Id.IsNotNull()))
                    cnt += new SearchRepository<AptifriedMeetingT, AptifriedMeetingSearchDto>(session).Search(search, false).Count();
          return new AptifriedMeetingCountResultsDto
            {
                SearchEntered = search,
                Count = cnt
            };
             
        }

    }
}