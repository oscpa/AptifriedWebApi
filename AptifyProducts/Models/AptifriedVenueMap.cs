using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedVenueMap : ClassMap<AptifriedVenue> {
		public AptifriedVenueMap() {
			Table("vwVenues");
			Id(x => x.Id);
			Map(x => x.Name);
			References(x => x.Parent).Column("ParentID");
			References(x => x.Address).Column("AddressID");
		}
	}
}