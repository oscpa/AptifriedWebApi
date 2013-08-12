#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMeetingTypeItemMap : ClassMap<AptifriedMeetingTypeItem>
    {
        public AptifriedMeetingTypeItemMap()
        {
            Table("vwMeetingTypeGroupItems");
            Id(x => x.Id);
            Map(x => x.Sequence);
            References(x => x.Group).Column("MeetingTypeGroupID");
            References(x => x.Type).Column("MeetingTypeID");
        }
    }
}