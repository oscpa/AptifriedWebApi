using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeResultMap : ClassMap<AptifriedKnowledgeResult> {
		public AptifriedKnowledgeResultMap() {
			Table("vwKnowledgeResults");

			Id(x => x.Id);

			Map(x => x.QuestionTreeId);
			Map(x => x.DateCreated);
			Map(x => x.DateUpdated);
			References(x => x.KnowledgeCaptureMode).Column("KnowledgeCaptureModeID");
			References(x => x.Person).Column("PersonID");
			Map(x => x.IsComplete);
		}
	}
}