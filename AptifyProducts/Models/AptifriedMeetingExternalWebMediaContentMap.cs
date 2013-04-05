using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingExternalWebMediaContentMap : ClassMap<AptifriedMeetingExternalWebMediaContent> {
        public AptifriedMeetingExternalWebMediaContentMap() {
            Table("vwMeetingExternalWebMediaContent");
            Id(x => x.Id);
            Map(x => x.MeetingId);
            References(x => x.MediaType).Column("WebMediaTypeID");
            Map(x => x.IFrameCode);
            Map(x => x.MediaFilePath);
            Map(x => x.RequireMeetingRegistration)
                .Access.Property().Default("1");
            Map(x => x.VideoId);
            ReadOnly();
        }
    }
}