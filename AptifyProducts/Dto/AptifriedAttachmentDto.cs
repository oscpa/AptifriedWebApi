using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedAttachmentDto {
        public int Id { get; set; }
        public string Name { get; set; }

        public AptifriedAttachmentCategoryDto Category { get; set; }
        public int EntityId { get; set; }
        public int RecordId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }
}