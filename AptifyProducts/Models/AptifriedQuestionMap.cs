using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionMap : ClassMap<AptifriedQuestion> {
		public AptifriedQuestionMap() {
			Table("vwQuestions");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.Text);
			References(x => x.KnowledgeCategory).Column("KnowledgeCategoryID");
			References(x => x.QuestionType).Column("QuestionTypeID");
			Map(x => x.Description);
		}
	}
}