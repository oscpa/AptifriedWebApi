namespace AptifyWebApi.Models.Dto.Meeting
{
    public class AptifriedMeetingTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool HasMeetings { get; set; }
    }
}