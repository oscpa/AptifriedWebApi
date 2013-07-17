#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedExamQuestionMap : ClassMap<AptifriedExamQuestion>
    {
        public AptifriedExamQuestionMap()
        {
            Table("vwExamQuestions");
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Points);
            Map(x => x.Question);
            Map(x => x.Answer);
            Map(x => x.Type);
            HasMany(x => x.PossibleAnswers).KeyColumn("ExamQuestionID");
            ReadOnly();
        }
    }
}