using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeAnswerMap : ClassMap<AptifriedKnowledgeAnswer> {
		public AptifriedKnowledgeAnswerMap() {
			Table("vwKnowledgeAnswers");

			Id(x => x.Id);

			Map(x => x.Text);
			References(x => x.KnowledgeCategory).Column("KnowledgeCategoryID");
			Map(x => x.Description);
		}
	}
}