using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedConferenceMeetingSessionSesSelController : AptifyEnabledApiController {
        public AptifriedConferenceMeetingSessionSesSelController(ISession session) : base(session) { }


        public IList<AptifriedMeetingDto> Get(int parentMeetingId) {
            string parentMeetingQuery =
                "Select	m.* " +
                "from	vwMeetings m " +
                "where	m.MeetingTypeID = 6 " +
                "and		m.Id in(select MeetingID from fnOscpaGetChildMeetings(:parentMeetingId)) ";

            var meetings = session.CreateSQLQuery(parentMeetingQuery)
                .AddEntity("m", typeof(AptifriedMeeting))
                .SetInt32("parentMeetingId", parentMeetingId)
                .List<AptifriedMeeting>();


            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}