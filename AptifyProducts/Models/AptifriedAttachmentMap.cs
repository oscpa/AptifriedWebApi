using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAttachmentMap : ClassMap<AptifriedAttachment>{
        public AptifriedAttachmentMap() {
            Table("vwAttachments");
            Id(x => x.Id);
            References(x => x.Category).Column("CategoryID");
            Map(x => x.DateCreated);
            Map(x => x.EntityId);
            Map(x => x.Name);
            Map(x => x.RecordId);
            Map(x => x.Status);
            ReadOnly();
        }
    }
}