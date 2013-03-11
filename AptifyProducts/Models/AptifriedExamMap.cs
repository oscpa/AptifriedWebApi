using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedExamMap : ClassMap<AptifriedExam> {
        public AptifriedExamMap() {
            Table("vwExams");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.NumberOfPoints);
            Map(x => x.NumberOfQuestions);
            Map(x => x.PassingScore);
            Map(x => x.Status);
            Map(x => x.PassType);
            HasMany(x => x.Questions).KeyColumn("ExamID");
            ReadOnly();
        }
    }
}