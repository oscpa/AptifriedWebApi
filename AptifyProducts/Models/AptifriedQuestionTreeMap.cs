using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionTreeMap : ClassMap<AptifriedQuestionTree> {
		public AptifriedQuestionTreeMap() {
			Table("vwQuestionTrees");

			Id(x => x.Id);

			Map(x => x.Name);
			Map(x => x.MaxQuestionsPerPage);
			Map(x => x.StartDate);
			Map(x => x.EndDate);
			Map(x => x.AllowPartialCompletion);
			Map(x => x.AllowDuplicates);
			References(x => x.RootQuestionBranch).Column("RootQuestionBranchID");
			References(x => x.KnowledgeCategory).Column("KnowledgeCategoryID");
			References(x => x.KnowledgeCaptureMode).Column("KnowledgeCaptureModeID");
			References(x => x.KnowledgeTrackingType).Column("KnowledgeTrackingTypeID");
			References(x => x.KnowledgeStatus).Column("KnowledgeStatusID");
			Map(x => x.Description);
			Map(x => x.EraseMessage);
		}
	}
}