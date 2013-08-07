namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingTypeItemDto
    {
        public virtual int Id { get; set; }
        public virtual AptifriedMeetingTypeGroupDto Group { get; set; }
        public virtual int Sequence { get; set; }
        public virtual AptifriedMeetingTypeDto Type { get; set; } 
    }
}