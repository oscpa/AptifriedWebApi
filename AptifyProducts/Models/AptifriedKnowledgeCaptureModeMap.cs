using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeCaptureModeMap : ClassMap<AptifriedKnowledgeCaptureMode> {
		public AptifriedKnowledgeCaptureModeMap() {
			Table("vwKnowledgeCaptureModes");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.Description);
			Map(x => x.SamplingMethod);
			Map(x => x.SamplingValue);
		}
	}
}