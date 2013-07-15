#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedAnswerSheetAnswerMap : ClassMap<AptifriedAnswerSheetAnswer>
    {
        public AptifriedAnswerSheetAnswerMap()
        {
            Table("vwAnswerSheetAnswers");
            Id(x => x.Id);
            Map(x => x.IsCorrect);
            Map(x => x.PointsEarned);
            Map(x => x.QuestionCode);
            Map(x => x.StudentAnswer);
            ReadOnly();
        }
    }
}