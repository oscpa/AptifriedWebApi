using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedEducationUnitAttachment {
		public virtual long Id { get; set; }
		public virtual long EducationUnitId { get; set; }
		public virtual long AttachmentId { get; set; }
	}
}