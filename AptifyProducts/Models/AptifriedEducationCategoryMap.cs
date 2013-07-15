#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedEducationCategoryMap : ClassMap<AptifriedEducationCategory>
    {
        public AptifriedEducationCategoryMap()
        {
            Table("vwEducationCategories");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Status);
            ReadOnly();
        }
    }
}