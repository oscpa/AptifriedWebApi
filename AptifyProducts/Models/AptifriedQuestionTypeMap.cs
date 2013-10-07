using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionTypeMap : ClassMap<AptifriedQuestionType> {
		public AptifriedQuestionTypeMap() {
			Table("vwQuestionTypes");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.MultipleAnswersToSave);
			Map(x => x.Xsl);
			Map(x => x.Description);
		}
	}
}