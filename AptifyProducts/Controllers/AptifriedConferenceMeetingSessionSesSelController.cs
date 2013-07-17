#region using

using System.Collections.Generic;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedConferenceMeetingSessionSesSelController : ApiController
    {
        private readonly ISession session;

        public AptifriedConferenceMeetingSessionSesSelController(ISession session)
        {
            this.session = session;
        }


        public IList<AptifriedMeetingDto> Get(int parentMeetingId)
        {
            string parentMeetingQuery =
                " Select	m.* " +
                " from	vwMeetings m " +
                " where	m.MeetingTypeID = 6 " +
                " and		m.Id in(select MeetingID from fnOscpaGetChildMeetings(:parentMeetingId)) " +
                " order by m.SessionNumber, m.StartDate ";

            var meetings = session.CreateSQLQuery(parentMeetingQuery)
                                  .AddEntity("m", typeof (AptifriedMeeting))
                                  .SetInt32("parentMeetingId", parentMeetingId)
                                  .List<AptifriedMeeting>();


            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}