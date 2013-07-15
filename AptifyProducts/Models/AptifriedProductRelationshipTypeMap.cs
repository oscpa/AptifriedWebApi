#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedProductRelationshipTypeMap : ClassMap<AptifriedProductRelationshipType>
    {
        public AptifriedProductRelationshipTypeMap()
        {
            Table("vwProductRelationshipTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}