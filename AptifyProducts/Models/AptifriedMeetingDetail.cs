using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedMeetingDetail {
		public virtual int Id { get; set; }
		public virtual int Sequence { get; set; }
		public virtual AptifriedPerson Attendee { get; set; }
		public virtual bool ShowNameOnList { get; set; }
		public virtual string BadgeName { get; set; }
		public virtual string BadgeCompanyName { get; set; }
		public virtual string BadgeTitle { get; set; }
		public virtual string RegistrationType { get; set; }
		public virtual AptifriedAttendeeStatus Status { get; set; }
		public virtual AptifriedProduct Product { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int OrderDetailId { get; set; }
	}
}