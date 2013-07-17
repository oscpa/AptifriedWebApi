#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto.SessionSelection
{
    public class AptifriedMeetingSessionLogDto
    {
        public int BillToId { get; set; }
        public int BillToCompanyId { get; set; }

        public int ShipToId { get; set; }
        public int ShipToCompanyId { get; set; }

        public IList<AptifriedNewMeetingSessionDto> NewSessions { get; set; }
        public IList<AptifriedCancelledMeetingSessionsDto> CancelledSessions { get; set; }
    }
}