﻿namespace AptifyWebApi.Models.SessionSelection
{
    public class AptifriedNewMeetingSession
    {
        public virtual int Id { get; set; }
        public virtual int OscpaMeetingSessionLogId { get; set; }

        public virtual int ProductId { get; set; }
        public virtual int AttendeeStatusId { get; set; }
    }
}