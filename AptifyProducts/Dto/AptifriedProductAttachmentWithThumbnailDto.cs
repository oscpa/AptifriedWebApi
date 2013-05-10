using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedProductAttachmentWithThumbnailDto {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Sequence { get; set; }
        public string ThumbnailName { get; set; }
        public string FileName { get; set; }
    }
}