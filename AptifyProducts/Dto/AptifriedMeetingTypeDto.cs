using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Meeting;

namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public bool HasMeetings { get; set; }

      
    }
}