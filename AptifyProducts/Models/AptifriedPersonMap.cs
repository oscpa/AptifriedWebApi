#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedPersonMap : ClassMap<AptifriedPerson>
    {
        public AptifriedPersonMap()
        {
            Table("vwPersonsTinyWebServices");

            Id(x => x.Id);

            Map(x => x.FirstName);
            Map(x => x.LastName);

            Map(x => x.Age);
            Map(x => x.CPEReportingGroup);
            Map(x => x.Gender);
            Map(x => x.AICPAMember).Column("AM4AICPAMember");
            Map(x => x.Email).Column("Email1");
            Map(x => x.MembershipAge);
            Map(x => x.PreferredAddress);
            Map(x => x.PreferredBillingAddress);
            Map(x => x.PreferredShippingAddress);
            Map(x => x.JoinDate);

            References(x => x.HomeAddress).Column("HomeAddressID");
            References(x => x.BusinessAddress).Column("AddressID");

            References(x => x.MemberType).Column("MemberTypeID");
            References(x => x.LicenseStatus).Column("LicenseStatusID");
            References(x => x.MemberClassificationType).Column("MemberClassificationTypeID");
            References(x => x.MemberStatusType).Column("MemberStatusTypeID");

            References(x => x.Company).Column("CompanyID");

            ReadOnly();
        }
    }
}