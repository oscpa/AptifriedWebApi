using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPerson {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual AptifriedAddress HomeAddress { get; set; }
        public virtual AptifriedAddress BusinessAddress { get; set; }
        public virtual AptifriedMemberType MemberType { get; set; }
		public virtual int Age { get; set; }
		public virtual string CPEReportingGroup { get; set; }
		public virtual string Gender { get; set; }
		public virtual bool AICPAMember { get; set; }
		public virtual AptifriedLicenseStatus LicenseStatus { get; set; }
		/*public virtual string MemberClassificationTypeIDName { get; set; }
		public virtual string MemberStatusTypeIDName { get; set; }
		public virtual string Email { get; set; }*/
    }
}