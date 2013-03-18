using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingTypeMap : ClassMap<AptifriedMeetingType> {
        public AptifriedMeetingTypeMap() {
            Table("vwMeetingTypes");
            Id(x => x.Id);
            Map(x => x.Name);
			Map(x => x.Description);
            ReadOnly();
        }
    }
}