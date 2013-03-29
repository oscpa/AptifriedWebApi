using AptifyWebApi.Dto;
using AptifyWebApi.Factories;
using AptifyWebApi.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingSearchCountController : AptifyEnabledApiController {

        public AptifriedMeetingSearchCountController(ISession session) : base(session) { }


        public AptifriedMeetingCountResultsDto Post(AptifriedMeetingSearchDto search) {
            int resultingCount = 0;
            var queryAndParams = MeetingSearchUtils.BuildFullQuery(search, true);
            var meetingQuery = session.CreateSQLQuery(queryAndParams.FullQuery);

            foreach (string paramKey in queryAndParams.QueryParams.Keys) {
                meetingQuery.SetParameter(paramKey, queryAndParams.QueryParams[paramKey].ToString());
            }

            var results = meetingQuery.List();
            if (results.Count > 0) {
                resultingCount = Convert.ToInt32(results[0]);
            }

            return new AptifriedMeetingCountResultsDto() {
                SearchEntered = search,
                Count = resultingCount
            };
        }

    }
}