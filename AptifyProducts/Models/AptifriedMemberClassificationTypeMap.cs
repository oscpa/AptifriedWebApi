#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMemberClassificationTypeMap : ClassMap<AptifriedMemberClassificationType>
    {
        public AptifriedMemberClassificationTypeMap()
        {
            Table("vwMemberClassificationTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.IsActive);
            Map(x => x.DefaultType);
            Map(x => x.OldID);
        }
    }
}