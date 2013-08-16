#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedAttachmentCategoryMap : ClassMap<AptifriedAttachmentCategory>
    {
        public AptifriedAttachmentCategoryMap()
        {
            Table("vwAttachmentCategories");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}