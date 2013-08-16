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
            //Map(x => x.BlobData).CustomType("BinaryBlob").Length(1048576).Not.Nullable(); //.CustomSqlType("varbinary(2147483647)").Length(int.MaxValue)
            Map(x => x.BlobData).CustomSqlType("VARBINARY(MAX)").Length(int.MaxValue);
            ReadOnly();
        }
    }
}