using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedVenueDto {
		public int Id { get; set; }
		public AptifriedVenueDto Parent { get; set; }
		public string Name { get; set; }
		public AptifriedAddressDto Address { get; set; }
	}
}