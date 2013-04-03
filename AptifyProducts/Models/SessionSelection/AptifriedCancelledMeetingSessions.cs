using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models.SessionSelection {
    public class AptifriedCancelledMeetingSessions {
        public virtual int Id { get; set; }
        public virtual int OscpaMeetingSessionLogId { get; set; }
        public virtual int Sequence { get; set; }

        public virtual int OrderMeetingDetailId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int StatusId { get; set; }
    }
}