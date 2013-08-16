#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedClassMap : ClassMap<AptifriedClass>
    {
        public AptifriedClassMap()
        {
            Table("vwClassesTiny");
            Id(x => x.Id);
            Map(x => x.Name).Column("WebName");
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            References(x => x.Location).Column("SchoolID");
            References(x => x.Course).Column("CourseID");
            References(x => x.Product).Column("ProductID");
            HasMany(x => x.Credits).KeyColumn("ClassID");
            ReadOnly();
        }
    }
}