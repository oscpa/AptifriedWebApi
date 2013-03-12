﻿using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedExamQuestionMap : ClassMap<AptifriedExamQuestion> {
        public AptifriedExamQuestionMap() {
            Table("vwExamQuestions");
            Id(x => x.Id);
            Map(x => x.Question);
            Map(x => x.Answer);
            Map(x => x.Type);
            ReadOnly();
        }
    }
}