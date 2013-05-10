using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedProductAttachmentWithThumbnailMap : ClassMap<AptifriedProductAttachmentWithThumbnail> {
        public AptifriedProductAttachmentWithThumbnailMap() {
            Table("vwProductAttachmentWithThumbnails");
            Id(x => x.Id);
            Map(x => x.ProductId);
            Map(x => x.Sequence);
            Map(x => x.FileName);
            Map(x => x.ThumbnailName);
        }
    }
}