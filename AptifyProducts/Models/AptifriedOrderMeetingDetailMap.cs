using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedOrderMeetingDetailMap : ClassMap<AptifriedOrderMeetingDetail> {
        public AptifriedOrderMeetingDetailMap() {
            Table("AptifriedOrderMeetingDetail");
            Id(x => x.Id);
            Map(x => x.AttendeeId);
            Map(x => x.MeetingId);
            Map(x => x.OrderId);
            Map(x => x.OrderDetailId);
            Map(x => x.ProductId);
            Map(x => x.StatusId);
            ReadOnly();
        }
    }
}