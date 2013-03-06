using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPersonMap : ClassMap<AptifriedPerson> {
        public AptifriedPersonMap() {
            Table("vwPersonsTinyWebServices");
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
			Map(x => x.Age);
			Map(x => x.CPEReportingGroup);
			Map(x => x.Gender);
			Map(x => x.AICPAMember).Column("AM4AICPAMember");
            References(x => x.HomeAddress).Column("HomeAddressID");
            References(x => x.BusinessAddress).Column("AddressID");
            References(x => x.MemberType).Column("MemberTypeID");
			References(x => x.LicenseStatus).Column("LicenseStatusID");
            ReadOnly();
        }
    }
}