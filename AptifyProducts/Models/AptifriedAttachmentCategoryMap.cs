using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAttachmentCategoryMap : ClassMap<AptifriedAttachmentCategory> {
        public AptifriedAttachmentCategoryMap() {
            Table("vwAttachmentCategories");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}