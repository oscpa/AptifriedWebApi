using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionBranchMap : ClassMap<AptifriedQuestionBranch> {
		public AptifriedQuestionBranchMap() {
			Table("vwQuestionBranches");

			Id(x => x.Id);

			Map(x => x.QuestionTreeId);
			References(x => x.Question).Column("QuestionID");
			References(x => x.NextQuestionBranch).Column("NextQuestionBranchID");
			Map(x => x.CheckAnswers);
			Map(x => x.PageBreak);
			Map(x => x.ForceSave);
			Map(x => x.AnswerRequired);
		}
	}
}