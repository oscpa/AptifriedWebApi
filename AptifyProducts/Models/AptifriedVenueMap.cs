#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedVenueMap : ClassMap<AptifriedVenue>
    {
        public AptifriedVenueMap()
        {
            Table("vwVenues");
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Parent).Column("ParentID");
            References(x => x.Address).Column("AddressID");
        }
    }
}