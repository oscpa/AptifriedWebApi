namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingEductionUnits
    {
        public virtual int Id { get; set; }
        public virtual int MeetingId { get; set; }
        public virtual AptifriedEducationCategory Category { get; set; }
        public virtual decimal EducationUnits { get; set; }
        public virtual string CeType { get; set; }
    }
}