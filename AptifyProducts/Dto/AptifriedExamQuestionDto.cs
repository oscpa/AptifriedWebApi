#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Dto
{
    public class AptifriedExamQuestionDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Points { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public IList<AptifriedExamQuestionPossibleAnswersDto> PossibleAnswers { get; set; }
    }
}