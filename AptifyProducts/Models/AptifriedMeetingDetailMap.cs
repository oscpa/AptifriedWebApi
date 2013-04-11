using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedMeetingDetailMap : ClassMap<AptifriedMeetingDetail> {
		public AptifriedMeetingDetailMap() {
			Table("vwOrderMeetDetail");
			Id(x => x.Id);
			Map(x => x.Sequence);
			Map(x => x.ShowNameOnList);
			Map(x => x.BadgeName);
			Map(x => x.BadgeCompanyName);
			Map(x => x.BadgeTitle);
			Map(x => x.RegistrationType);
            Map(x => x.OrderId);
            Map(x => x.OrderDetailId);
			Map(x => x.MeetingId);
			References(x => x.Attendee).Column("AttendeeID");
			References(x => x.Status).Column("StatusID");
			References(x => x.Product).Column("ProductID");
			References(x => x.Meeting).Column("MeetingID");
		}
	}
}