using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAttachment {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual AptifriedAttachmentCategory Category { get; set; }
        public virtual int EntityId { get; set; }
        public virtual int RecordId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Status { get; set; }
		public virtual byte[] BlobData { get; set; }
    }
}