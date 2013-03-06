using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedMemberStatusTypeDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string OldID { get; set; }
		public bool IsActive { get; set; }
		public bool IsBenefitEligible { get; set; }
		public string DefaultMemberType { get; set; }
		public string DefaultType { get; set; }
		public bool IsMember { get; set; }
		public Guid UniqueID { get; set; }
	}
}