using System.Text.RegularExpressions;
using AptifyWebApi.Models.Meeting;

namespace AptifyWebApi.Models
{
    public class AptifriedMeetingTypeItem
    {
        public virtual int Id { get; set; }
        public virtual AptifriedMeetingTypeGroup Group { get; set; }
        public virtual int Sequence { get; set; }
        public virtual AptifriedMeetingType Type { get; set; } 
    }
}