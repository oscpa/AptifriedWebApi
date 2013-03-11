using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingEductionUnitsMap  : ClassMap<AptifriedMeetingEductionUnits> {
        public AptifriedMeetingEductionUnitsMap() {
            Table("vwMeetingEducationUnits");
            Id(x => x.Id);
            Map(x => x.EducationUnits);
            Map(x => x.CeType);
            Map(x => x.MeetingId);
            References(x => x.Category).Column("EducationCategoryID");
            ReadOnly();
        }
    }
}