using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeCaptureMode {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual string SamplingMethod { get; set; }
		public virtual int SamplingValue { get; set; }
	}
}