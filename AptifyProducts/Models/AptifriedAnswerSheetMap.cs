#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedAnswerSheetMap : ClassMap<AptifriedAnswerSheet>
    {
        public AptifriedAnswerSheetMap()
        {
            Table("vwAnswerSheets");
            Id(x => x.Id);
            Map(x => x.DateRecorded);
            Map(x => x.ExamId);
            Map(x => x.PercentScore);
            Map(x => x.Score);
            Map(x => x.SerialNumber);
            Map(x => x.Status);
            Map(x => x.MeetingId);
            References(x => x.Student).Column("StudentID");
            HasMany(x => x.Answers).KeyColumn("AnswerSheetID");
            ReadOnly();
        }
    }
}