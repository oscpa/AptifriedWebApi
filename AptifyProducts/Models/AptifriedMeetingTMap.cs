using System;
using NHibernate.Criterion;

#region using

using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTMap : ClassMap<AptifriedMeetingT>
    {
        public AptifriedMeetingTMap()
        {
            Table("vwMeetingsTiny");
            Id(x => x.Id);
            Map(x => x.MeetingTitle);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.OpenTime)
                .CustomType("TimeAsTimeSpan");
            Map(x => x.ClassLevelId);
            Map(x => x.Relevance);
            Map(x => x.StatusId).Column("StatusID");
            //Map(x => x.Miles).Nullable();
            References(x => x.Product).Column("ProductID");
            References(x => x.Location).Column("AddressID");
            HasMany(x => x.Credits).KeyColumn("MeetingID");
            References(x => x.Venue).Column("VenueID");
            References(x => x.TypeItem).Column("MeetingTypeGroupId");

        }
    }
}