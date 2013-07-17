using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedCPECertificateDto {
		public AptifriedAttachmentDto Attachment { get; set; }
		public string Base64Data { get; set; }
	}
}