#region using

using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingMap : ClassMap<AptifriedMeeting>
    {
        public AptifriedMeetingMap()
        {
            Table("vwMeetingsTiny");
            Id(x => x.Id);
            Map(x => x.MeetingTitle);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.OpenTime)
                .CustomType("TimeAsTimeSpan");
            Map(x => x.ClassLevelId);
            Map(x => x.Rank).Nullable();
            References(x => x.Product).Column("ProductID");
            References(x => x.Status).Column("StatusID");
            References(x => x.Location).Column("AddressID");
            HasMany(x => x.Credits).KeyColumn("MeetingID");
            References(x => x.Venue).Column("VenueID");
            References(x => x.TypeItem).Column("MeetingTypeGroupId");

        }
    }
}