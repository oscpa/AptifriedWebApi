using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models.SessionSelection {
    public class AptifriedMeetingSessionLog {
        public virtual int Id { get; set; }

        public virtual int BillToId { get; set; }
        public virtual int BillToCompanyId { get; set; }
        public virtual int ShipToId { get; set; }
        public virtual int ShipToCompanyId { get; set; }
    }
}