using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionTreeKnowledgeDeliveryTypeMap : ClassMap<AptifriedQuestionTreeKnowledgeDeliveryType> {
		public AptifriedQuestionTreeKnowledgeDeliveryTypeMap() {
			Table("vwQuestionTreeKnowledgeDeliveryTypes");

			Id(x => x.Id);

			Map(x => x.QuestionTreeId);
			Map(x => x.Sequence);
			References(x => x.KnowledgeDeliveryType).Column("KnowledgeDeliveryTypeID");
			Map(x => x.AccessPassword);
		}
	}
}