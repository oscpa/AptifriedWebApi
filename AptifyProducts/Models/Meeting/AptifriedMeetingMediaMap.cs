#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingMediaMap : ClassMap<AptifriedMeetingMedia>
    {
        public AptifriedMeetingMediaMap()
        {
            Table("vwMeetingMedia");
            Id(x => x.Id);
            Map(x => x.IframeCode);
            Map(x => x.MediaFileKey);
            ReadOnly();
        }
    }
}