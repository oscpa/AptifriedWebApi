using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionBranchAnswerBranchMap : ClassMap<AptifriedQuestionBranchAnswerBranch> {
		public AptifriedQuestionBranchAnswerBranchMap() {
			Table("vwQuestionBranchAnswerBranches");

			Id(x => x.Id);

			References(x => x.QuestionBranch).Column("QuestionBranchID");
			Map(x => x.Sequence);
			References(x => x.KnowledgeAnswer).Column("KnowledgeAnswerID");
			References(x => x.NextQuestionBranch).Column("NextQuestionBranchID");
		}
	}
}