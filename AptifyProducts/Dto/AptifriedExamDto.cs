using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedExamDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PassType { get; set; }
        public decimal PassingScore { get; set; }
        public decimal NumberOfQuestions { get; set; }
        public decimal NumberOfPoints { get; set; }
        public string Status { get; set; }
        public IList<AptifriedExamQuestionDto> Questions { get; set; }
    }
}