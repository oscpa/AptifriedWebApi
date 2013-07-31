
 #region using

using System.Collections.Generic;
using System.Linq;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
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


        public AptifriedMeetingCountResultsDto Post(AptifriedMeetingSearchDto search)
        {
           var res = new SearchRepository<AptifriedMeeting,AptifriedMeetingSearchDto>(session).Search(search, false);

            if (search.HasTypeGroup)
                res = res.Where(x => search.MeetingTypeGroupIds.Contains(x.TypeGroup.Id));
           
            return new AptifriedMeetingCountResultsDto
            {
                SearchEntered = search,
                Count = res.IsNotNull() ? res.Count() : 0
            };
        }
    }
}