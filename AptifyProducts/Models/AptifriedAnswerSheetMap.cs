using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAnswerSheetMap : ClassMap<AptifriedAnswerSheet> {
        public AptifriedAnswerSheetMap() {
            Table("vwAnswerSheets");
            Id(x => x.Id);
            Map(x => x.DateRecorded);
            Map(x => x.ExamId);
            Map(x => x.PercentScore);
            Map(x => x.PointScore);
            Map(x => x.SerialNumber);
            Map(x => x.Status);
            References(x => x.Student).Column("StudentID");
            HasMany(x => x.Answers).KeyColumn("AnswerSheetID");
            ReadOnly();
        }
    }
}