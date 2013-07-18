namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingDetailDto
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public AptifriedPersonDto Attendee { get; set; }
        public bool ShowNameOnList { get; set; }
        public string BadgeName { get; set; }
        public string BadgeCompanyName { get; set; }
        public string BadgeTitle { get; set; }
        public string RegistrationType { get; set; }
        public AptifriedAttendeeStatusDto Status { get; set; }
        public AptifriedProductDto Product { get; set; }
        public int MeetingId { get; set; }
        public AptifriedMeetingDto Meeting { get; set; }
    }
}