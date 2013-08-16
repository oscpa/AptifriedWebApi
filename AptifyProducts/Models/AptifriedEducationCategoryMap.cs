using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedEducationCategoryMap : ClassMap<AptifriedEducationCategory> {
        public AptifriedEducationCategoryMap() {
            Table("vwEducationCategories");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Code);
			Map(x => x.Status);
            ReadOnly();
        }
    }
}