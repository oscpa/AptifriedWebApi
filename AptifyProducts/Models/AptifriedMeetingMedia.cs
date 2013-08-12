namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingMedia
    {
        public virtual int Id { get; set; }
        public virtual string MediaFileKey { get; set; }
        public virtual string IframeCode { get; set; }
    }
}