#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedWebShoppingCartTypeMap : ClassMap<AptifriedWebShoppingCartType>
    {
        public AptifriedWebShoppingCartTypeMap()
        {
            Table("vwWebShoppingCartTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}