#region using

using System.Collections.Generic;
using AptifyWebApi.Dto;
using AptifyWebApi.Dto.SessionSelection;
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