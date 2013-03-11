using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAnswerSheetAnswerMap : ClassMap<AptifriedAnswerSheetAnswer> {
        public AptifriedAnswerSheetAnswerMap() {
            Table("vwAnswerSheetAnswers");
            Id(x => x.Id);
            Map(x => x.IsCorrect);
            Map(x => x.PointsEarned);
            Map(x => x.QuestionCode);
            Map(x => x.StudentAnswer);
            ReadOnly();
        }
    }
}