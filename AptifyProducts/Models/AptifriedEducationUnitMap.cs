using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedEducationUnitMap : ClassMap<AptifriedEducationUnit> {
        public AptifriedEducationUnitMap() {
            Table("vwEducationUnits");
            Id(x => x.Id);
            References(x => x.Person).Column("PersonID");
            Map(x => x.DateEarned);
            Map(x => x.DateExpires);
            Map(x => x.SelectDate);
            References(x => x.EducationCategory).Column("EducationCategoryID");
            Map(x => x.Status);
            Map(x => x.EducationUnits);
            Map(x => x.Source);
            Map(x => x.ExternalSource);
            Map(x => x.ExternalSourceDescription);
            Map(x => x.ExternalSourceVerified);
            Map(x => x.ExternalCPECity);
            Map(x => x.ExternalCPEInstructor);
            Map(x => x.ExternalCPESponsor);
            References(x => x.Meeting).Column("MeetingID");
            References(x => x.CpeCreditAdjustmentType).Column("CPECreditAdjustmentTypeID");
            Map(x => x.Deactivate);
            ReadOnly();
        }
    }
}