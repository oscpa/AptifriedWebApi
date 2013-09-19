using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedProductPersonNoteMap : ClassMap<AptifriedProductPersonNote> {
		public AptifriedProductPersonNoteMap() {
			Table("vwProductPersonNotes");

			Id(x => x.Id);

			References(x => x.Product).Column("ProductID");
			References(x => x.Person).Column("PersonID");

			Map(x => x.Body);
			Map(x => x.DateCreated);
			Map(x => x.DateUpdated);
            Map(x => x.IsActive);
		}
	}
}