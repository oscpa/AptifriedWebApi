
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

            //TODO: Refactor.  Add post call for CreditTypesCount
            if(search.HasMeetingTypeItems)
                foreach (var mType in search.MeetingTypes.Where(mType => mType.Group.IsNotNull() && mType.Group.Id.IsNotNull()))
                {
                    if (SearchCounts.MeetingSearch.Groups.NeedsUpdate(mType.Group.Id))
                        SearchCounts.MeetingSearch.Groups.Update(mType.Group.Id,
                            new SearchRepository<AptifriedMeetingT, AptifriedMeetingSearchDto>(session).Search(
                                search, false).Count());

                    cnt += SearchCounts.MeetingSearch.Groups.GetCount(mType.Group.Id);
                }

            if (search.HasMeetingTypeItems)
                foreach (var mType in search.MeetingTypes.Where(mType => mType.Type.IsNotNull() && mType.Type.Id.IsNotNull()))
                {
                    if (SearchCounts.MeetingSearch.Types.NeedsUpdate(mType.Type.Id))
                        SearchCounts.MeetingSearch.Types.Update(mType.Type.Id,
                            new SearchRepository<AptifriedMeetingT, AptifriedMeetingSearchDto>(session).Search(
                                search, false).Count());

                    cnt += SearchCounts.MeetingSearch.Types.GetCount(mType.Type.Id);
                }
            /*
            if(search.HasCreditTypes)
                foreach (var cType in search.CreditTypes)
                {
                    if (SearchCounts.MeetingSearch.CreditTypes.NeedsUpdate(cType.Id))
                        SearchCounts.MeetingSearch.CreditTypes.Update(cType.Id,
                            new SearchRepository<AptifriedMeeting, AptifriedMeetingSearchDto>(session).Search(
                                search, false).Count());

                    cnt += SearchCounts.MeetingSearch.CreditTypes.GetCount(cType.Id);
                }
            */
          return new AptifriedMeetingCountResultsDto
            {
                SearchEntered = search,
                Count = cnt
            };
             
        }

    }
}