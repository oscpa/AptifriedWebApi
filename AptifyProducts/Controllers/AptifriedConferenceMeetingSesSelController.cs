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
    public class AptifriedConferenceMeetingSesSelController : AptifyEnabledApiController
    {
        public AptifriedConferenceMeetingSesSelController(ISession session) : base(session)
        {
        }

        public IList<AptifriedMeetingDto> Get()
        {
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
                                  .AddEntity("M", typeof (AptifriedMeeting))
                                  .SetInt32("attendee", AptifyUser.PersonId)
                                  .List<AptifriedMeeting>();

            return Mapper.Map(meetings, new List<AptifriedMeetingDto>());
        }
    }
}