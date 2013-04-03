using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedConferenceMeetingsSesSelController : AptifyEnabledApiController {
        public AptifriedConferenceMeetingsSesSelController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Get() {

            // Shamelessly lifted from spOSCPAMtgWizardGetEligibleMtg
            string conferenceMeetingQuery =
                " Select	M.* " +
                " from	vwMeetingsTiny M " +
                " Join	vwProductsTiny P ON M.ProductID = P.ID " +
                " WHERE P.ID IN " +
                "	(	Select ProductID " +
                "		from aptify.dbo.vwOrderMeetDetail " +
                "		where AttendeeID = :attendee AND StatusID <> 4 " +
                "		AND ProductID IN (SELECT ParentID From Aptify.dbo.vwProducts Where ProductTypeID=1)) ";


            var meetings = session.CreateSQLQuery(conferenceMeetingQuery)
                .AddEntity("M", typeof(AptifriedMeeting))
                .SetInt32("attendee", AptifyUser.PersonId)
                .List<AptifriedMeeting>();

            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
            
        }
    }
}