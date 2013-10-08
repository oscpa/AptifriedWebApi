using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeResultDetailMap : ClassMap<AptifriedKnowledgeResultDetail> {
		public AptifriedKnowledgeResultDetailMap() {
			Table("vwKnowledgeResultDetails");

			Id(x => x.Id);

			References(x => x.KnowledgeResult).Column("KnowledgeResultID");
			Map(x => x.Sequence);
			References(x => x.QuestionTree).Column("QuestionTreeID");
			References(x => x.QuestionBranch).Column("QuestionBranchID");
			References(x => x.Question).Column("QuestionID");
			References(x => x.KnowledgeAnswer).Column("KnowledgeAnswerID");
			Map(x => x.KnowledgeAnswerValue);
			Map(x => x.HtmlName);
		}
	}
}