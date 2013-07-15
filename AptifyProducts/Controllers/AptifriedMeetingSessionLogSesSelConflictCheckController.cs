#region using

using System.Collections.Generic;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Models.Dto.Meeting;
using AptifyWebApi.Models.Dto.SessionSelection;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingSessionLogSesSelConflictCheckController :
        AptifriedMeetingSessionLogSesSelBaseController
    {
        public AptifriedMeetingSessionLogSesSelConflictCheckController(ISession session) : base(session)
        {
        }


        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSessionLogDto sessionChanges)
        {
            var meetingSessionEntity = base.AddRegistrationsAndCancellationsToMeetingSessionLog(sessionChanges);
            var meetingsInConflict = base.FindAnyConflictsInSessionChanges(meetingSessionEntity);

            return meetingsInConflict;
        }
    }
}