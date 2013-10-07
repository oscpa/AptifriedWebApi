using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedKnowledgeParticipantMap : ClassMap<AptifriedKnowledgeParticipant> {
		public AptifriedKnowledgeParticipantMap() {
			Table("vwKnowledgeParticipants");

			Id(x => x.Id);

			References(x => x.Person).Column("PersonID");
			Map(x => x.QuestionTreeId);
			Map(x => x.DateCreated);
			Map(x => x.DateUpdated);
			References(x => x.KnowledgeResult).Column("KnowledgeResultID");
			Map(x => x.IsComplete);
		}
	}
}