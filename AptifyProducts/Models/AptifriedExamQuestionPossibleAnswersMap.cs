#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedExamQuestionPossibleAnswersMap : ClassMap<AptifriedExamQuestionPossibleAnswers>
    {
        public AptifriedExamQuestionPossibleAnswersMap()
        {
            Table("vwExamQuestionAnswers");
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Description);
        }
    }
}