using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Factories;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingSearchController : AptifyEnabledApiController {

        public AptifriedMeetingSearchController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSearchDto search) {
            IList<AptifriedMeetingDto> resultingMeetings = new List<AptifriedMeetingDto>();

            var queryAndParams = MeetingSearchUtils.BuildFullQuery(search);

            var meetingQuery = session.CreateSQLQuery(queryAndParams.FullQuery)
                .AddEntity("mt", typeof(AptifriedMeeting));

            foreach (string paramKey in queryAndParams.QueryParams.Keys) {
                meetingQuery.SetParameter(paramKey, queryAndParams.QueryParams[paramKey].ToString());
            }

            var meetings = meetingQuery.List<AptifriedMeeting>();
            resultingMeetings = Mapper.Map(meetings, new List<AptifriedMeetingDto>());

            return resultingMeetings;
        }



    }
}