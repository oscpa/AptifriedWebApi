namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedAnswerSheetAnswer
    {
        public virtual int Id { get; set; }
        public virtual string QuestionCode { get; set; }
        public virtual string StudentAnswer { get; set; }
        public virtual bool IsCorrect { get; set; }
        public virtual decimal PointsEarned { get; set; }
    }
}