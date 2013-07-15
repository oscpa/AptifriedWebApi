#region using

using System.Collections.Generic;
using System.Web.Http;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Models.Dto.Meeting;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedConferenceAttendanceSesSelController : AptifyEnabledApiController
    {
        public AptifriedConferenceAttendanceSesSelController(ISession session) : base(session)
        {
        }

        public IList<AptifriedMeetingDto> Get(int parentMeetingId)
        {
            // had to break this out into a query for the function i call. not sure how to wire them up
            string attendanceQuery =
                " select mt.* " +
                " from vwMeetingsTiny mt " +
                " where  " +
                " mt.ID in ( " +
                " 	select omd.MeetingID " +
                " 	from vwordermeetDetail omd " +
                "   join dbo.vwMeetingsTiny mt on omd.MeetingID = mt.ID " +
                "	where omd.AttendeeID = :attendeeId and omd.StatusID <> 4 and mt.MeetingTypeID = 6" +
                "   and mt.ID in (select MeetingId from dbo.fnOscpaGetChildMeetings(:parentMeetingId)) )";

            var meetings = session.CreateSQLQuery(attendanceQuery)
                                  .AddEntity("mt", typeof (AptifriedMeeting))
                                  .SetInt32("attendeeId", AptifyUser.PersonId)
                                  .SetInt32("parentMeetingId", parentMeetingId)
                                  .List<AptifriedMeeting>();

            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}