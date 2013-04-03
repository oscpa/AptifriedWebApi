using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {
    public class AptifriedConferenceMeetingSessionSesSelController : ApiController {

        private ISession session;

        public AptifriedConferenceMeetingSessionSesSelController(ISession session) {
            this.session = session;
        }


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