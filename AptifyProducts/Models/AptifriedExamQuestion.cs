#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedExamQuestion
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual int Points { get; set; }
        public virtual string Type { get; set; }
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
        public virtual IList<AptifriedExamQuestionPossibleAnswers> PossibleAnswers { get; set; }
    }
}