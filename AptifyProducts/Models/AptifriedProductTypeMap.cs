#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedProductTypeMap : ClassMap<AptifriedProductType>
    {
        public AptifriedProductTypeMap()
        {
            Table("vwProductTypes");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}