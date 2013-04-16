using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPerson {
        public virtual int Id { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

		public virtual int Age { get; set; }
		public virtual string CPEReportingGroup { get; set; }
		public virtual string Gender { get; set; }
		public virtual bool AICPAMember { get; set; }
		public virtual string Email { get; set; }
		public virtual int MembershipAge { get; set; }
		public virtual string PreferredAddress { get; set; }
		public virtual string PreferredBillingAddress { get; set; }
		public virtual string PreferredShippingAddress { get; set; }
		public virtual DateTime JoinDate { get; set; }

        public virtual AptifriedAddress HomeAddress { get; set; }
        public virtual AptifriedAddress BusinessAddress { get; set; }

        public virtual AptifriedMemberType MemberType { get; set; }
		public virtual AptifriedLicenseStatus LicenseStatus { get; set; }
		public virtual AptifriedMemberClassificationType MemberClassificationType { get; set; }
		public virtual AptifriedMemberStatusType MemberStatusType { get; set; }

		public virtual AptifriedCompany Company { get; set; }
    }
}
