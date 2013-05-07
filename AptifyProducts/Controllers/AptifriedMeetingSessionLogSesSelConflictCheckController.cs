using AptifyWebApi.Dto.SessionSelection;
using AptifyWebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingSessionLogSesSelConflictCheckController : AptifriedMeetingSessionLogSesSelBaseController {
        public AptifriedMeetingSessionLogSesSelConflictCheckController(ISession session) : base(session) { }


        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSessionLogDto sessionChanges) {

            var meetingSessionEntity = base.AddRegistrationsAndCancellationsToMeetingSessionLog(sessionChanges);
            var meetingsInConflict = base.FindAnyConflictsInSessionChanges(meetingSessionEntity);

            return meetingsInConflict;
        }
    }
}