using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedProductQuestionTreeMap : ClassMap<AptifriedProductQuestionTree> {
		public AptifriedProductQuestionTreeMap() {
			Table("vwProductQuestionTrees");

			Id(x => x.Id);

			Map(x => x.Sequence);
			Map(x => x.ProductId);
			Map(x => x.QuestionTreeId);
		}
	}
}