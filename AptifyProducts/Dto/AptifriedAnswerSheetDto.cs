using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedAnswerSheetDto {

        public int Id { get; set; }
        public AptifriedPersonDto Student { get; set; }
        public int ExamId { get; set; }
        public string Status { get; set; }
        public DateTime DateRecorded { get; set; }
        public decimal Score { get; set; }
        public decimal PercentScore { get; set; }
        public string SerialNumber { get; set; }
		public int MeetingId { get; set; }

        public IList<AptifriedAnswerSheetAnswerDto> Answers { get; set; }
    }
}