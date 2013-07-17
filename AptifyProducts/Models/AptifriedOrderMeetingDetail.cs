using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedOrderMeetingDetail {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int OrderDetailId { get; set; }
        public virtual int AttendeeId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int MeetingId { get; set; }
    }
}