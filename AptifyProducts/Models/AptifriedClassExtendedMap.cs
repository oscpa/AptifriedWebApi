#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedClassExtendedMap : ClassMap<AptifriedClassExtended>
    {
        public AptifriedClassExtendedMap()
        {
            Table("vwClasses");
            Id(x => x.Id);
            Map(x => x.Name).Column("WebName");
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            References(x => x.Location).Column("SchoolID");
            References(x => x.Course).Column("CourseID");
            References(x => x.Product).Column("ProductID");
            HasMany(x => x.Credits).KeyColumn("ClassID");

            Map(x => x.MarketingCopy);
            ReadOnly();
        }
    }
}