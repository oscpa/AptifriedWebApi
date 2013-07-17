#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMemberTypeMap : ClassMap<AptifriedMemberType>
    {
        public AptifriedMemberTypeMap()
        {
            Table("vwMemberTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.IsMember);
        }
    }
}