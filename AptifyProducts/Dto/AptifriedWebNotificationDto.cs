#region using

using System;

#endregion

namespace AptifyWebApi.Models.Dto
{
    public class AptifriedWebNotificationDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool Seen { get; set; }
        public int PersonId { get; set; }
    }
}