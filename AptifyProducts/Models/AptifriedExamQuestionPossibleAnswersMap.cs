using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedExamQuestionPossibleAnswersMap : ClassMap<AptifriedExamQuestionPossibleAnswers> {
        public AptifriedExamQuestionPossibleAnswersMap() {
            Table("vwExamQuestionAnswers");
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Description);
        }
    }
}