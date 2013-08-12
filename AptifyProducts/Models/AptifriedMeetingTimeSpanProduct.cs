namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTimeSpanProduct
    {
        public virtual int Id { get; set; }
        public virtual int Sequence { get; set; }
        public virtual AptifriedProduct Product { get; set; }
    }
}