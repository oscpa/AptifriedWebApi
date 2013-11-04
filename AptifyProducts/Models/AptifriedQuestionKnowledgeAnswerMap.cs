using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionKnowledgeAnswerMap : ClassMap<AptifriedQuestionKnowledgeAnswer> {
		public AptifriedQuestionKnowledgeAnswerMap() {
			Table("vwQuestionKnowledgeAnswers");

			Id(x => x.Id);

			//References(x => x.Question).Column("QuestionID");
			Map(x => x.Sequence);
			References(x => x.KnowledgeAnswer).Column("KnowledgeAnswerID");
			Map(x => x.AnswerRequired);
		}
	}
}