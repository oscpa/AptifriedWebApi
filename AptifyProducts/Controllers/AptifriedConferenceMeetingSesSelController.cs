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
    public class AptifriedConferenceMeetingSesSelController : AptifyEnabledApiController {
        public AptifriedConferenceMeetingSesSelController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Get() {

            // Shamelessly lifted from spOSCPAMtgWizardGetEligibleMtg


			/**
			 * JP - 7/23/13 - Fused with CJ's code; see https://github.com/oscpa/Store-Pathfix/issues/350
			 * Don'tchall love inline SQL? Gag.
			 **/
			string conferenceMeetingQuery =
				" Select	M.* " +
				" from	vwMeetingsTiny M " +
				" Join	vwProductsTiny P ON M.ProductID = P.ID " +
				" WHERE P.ID IN " +
				"	(	Select ProductID " +
				"		from aptify.dbo.vwOrderMeetDetail " +
				"		where AttendeeID = :attendee AND StatusID in (1, 2, 8, 9, 10) " +
				"		AND ProductID IN (SELECT ParentID From Aptify.dbo.vwProducts Where ProductTypeID=1)) " +
				"and P.IsSold = 1 " +
				"and m.MeetingTypeID = 3 " +
				"and m.StatusID = 1 " +
				"and m.StartDate >= getdate() " +
				"and p.ProductTypeID = 1";

            var meetings = session.CreateSQLQuery(conferenceMeetingQuery)
                .AddEntity("M", typeof(AptifriedMeeting))
                .SetInt32("attendee", AptifyUser.PersonId)
                .List<AptifriedMeeting>();

            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}