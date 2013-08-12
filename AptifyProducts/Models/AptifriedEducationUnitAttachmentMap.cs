using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedEducationUnitAttachmentMap : ClassMap<AptifriedEducationUnitAttachment> {
		public AptifriedEducationUnitAttachmentMap() {
			Table("vwEducationUnitAttachments");
			Id(x => x.Id);
			Map(x => x.EducationUnitId);
			Map(x => x.AttachmentId);
		}
	}
}