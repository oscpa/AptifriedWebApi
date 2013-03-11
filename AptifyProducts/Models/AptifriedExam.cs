﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedExam {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
        public virtual decimal NumberOfQuestions { get; set; }
        public virtual decimal NumberOfPoints { get; set; }
        public virtual decimal PassingScore { get; set; }
        public virtual string PassType { get; set; }
        public virtual IList<AptifriedExamQuestion> Questions { get; set; }
    }
}