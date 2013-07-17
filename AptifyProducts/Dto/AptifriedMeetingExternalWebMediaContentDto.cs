#region using

using System;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedMeetingExternalWebMediaContentDto
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public AptifriedMeetingWebMediaTypeDto MediaType { get; set; }
        public string MediaFilePath { get; set; }
        public string IFrameCode { get; set; }
        public string MediaImagePath { get; set; }
        public bool RequireMeetingRegistration { get; set; }
        public string VideoId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}