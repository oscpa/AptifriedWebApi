﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedPersonDto {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AptifriedAddressDto HomeAddress { get; set; }
        public AptifriedAddressDto BusinessAddress { get; set; }
        public AptifriedMemberTypeDto MemberType { get; set; }
		public int Age { get; set; }
		public string CPEReportingGroup { get; set; }
		public string Gender { get; set; }
		public bool AICPAMember { get; set; }
		public AptifriedLicenseStatusDto LicenseStatus { get; set; }
		/*public string MemberClassificationTypeIDName { get; set; }
		public string MemberStatusTypeIDName { get; set; }
		public string Email { get; set; }*/
    }
}