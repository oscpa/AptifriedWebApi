<<<<<<< HEAD
﻿#region using

using System.Linq;
using AptifyWebApi.Dto;
using AptifyWebApi.Repository;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingSearchCountController : AptifyEnabledApiController
    {
        public AptifriedMeetingSearchCountController(ISession session) : base(session)
        {
        }


        public AptifriedMeetingCountResultsDto Post(AptifriedMeetingSearchDto search)
        {
            var res = new SearchRepository(session).Search(search, false);

            return new AptifriedMeetingCountResultsDto
                {
                    SearchEntered = search,
                    Count = res.Count()
                };
        }
    }
=======
﻿#region using

using System.Linq;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Models.Dto.Meeting;
using AptifyWebApi.Repository;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingSearchCountController : AptifyEnabledApiController
    {
        public AptifriedMeetingSearchCountController(ISession session) : base(session)
        {
        }


        public AptifriedMeetingCountResultsDto Post(AptifriedMeetingSearchDto search)
        {
            var res = new SearchRepository().Search(session, search, false);

            return new AptifriedMeetingCountResultsDto
                {
                    SearchEntered = search,
                    Count = res.Count()
                };
        }
    }
>>>>>>> origin/ac-init
}