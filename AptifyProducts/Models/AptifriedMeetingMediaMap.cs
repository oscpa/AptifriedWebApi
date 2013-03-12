using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingMediaMap : ClassMap<AptifriedMeetingMedia> {
        public AptifriedMeetingMediaMap() {
            Table("vwMeetingMedia");
            Id(x => x.Id);
            Map(x => x.IframeCode);
            Map(x => x.MediaFileKey);
            ReadOnly();
        }
    }
}