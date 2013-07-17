#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingExternalWebMediaContentMap : ClassMap<AptifriedMeetingExternalWebMediaContent>
    {
        public AptifriedMeetingExternalWebMediaContentMap()
        {
            Table("vwMeetingExternalWebMediaContent");
            Id(x => x.Id);
            Map(x => x.MeetingId);
            References(x => x.MediaType).Column("WebMediaTypeID");
            Map(x => x.IFrameCode);
            Map(x => x.MediaFilePath);
            Map(x => x.RequireMeetingRegistration).CustomSqlType("bit");
            Map(x => x.VideoId);
            Map(x => x.CreateDate);
            Map(x => x.DateRecorded);
            ReadOnly();
        }
    }
}