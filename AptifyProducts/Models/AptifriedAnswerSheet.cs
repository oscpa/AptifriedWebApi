using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedAnswerSheet {
        public virtual int Id { get; set; }
        public virtual AptifriedPerson Student { get; set; }
        public virtual int ExamId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime DateRecorded { get; set; }
        public virtual decimal PointScore { get; set; }
        public virtual decimal PercentScore { get; set; }
        public virtual string SerialNumber { get; set; }

        public virtual IList<AptifriedAnswerSheetAnswer> Answers { get; set; }
    }
}