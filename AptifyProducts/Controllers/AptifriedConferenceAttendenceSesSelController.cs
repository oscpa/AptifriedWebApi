﻿using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedConferenceAttendenceSesSelController : AptifyEnabledApiController {
        public AptifriedConferenceAttendenceSesSelController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Get(int parentMeetingId) {

            string attendanceQuery =
                " select mt.* " +
                " from vwMeetingsTiny mt "+
                " where mt.MeetingTypeID = 6  " +
                " and	mt.ID in (select MeetingId from dbo.fnOscpaGetChildMeetings(:parentMeetingId)) " +
                " and	mt.ID in ( " +
                " 	select omd.MeetingID " +
                " 	from vwordermeetDetail omd " +
                "	where omd.AttendeeID = :attendeeId and omd.StatusID <> 1) ";

            var meetings = session.CreateSQLQuery(attendanceQuery)
                .AddEntity("mt", typeof(AptifriedMeeting))
                .SetInt32("attendeeId", AptifyUser.PersonId)
                .SetInt32("parentMeetingId", parentMeetingId)
                .List<AptifriedMeeting>();

            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}