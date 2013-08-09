using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedEducationUnitAttachmentDto {
		public long Id { get; set; }
		public long EducationUnitId { get; set; }
		public long AttachmentId { get; set; }
	}
}