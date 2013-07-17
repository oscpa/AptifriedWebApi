#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedSecurityKeyMap : ClassMap<AptifriedSecurityKey>
    {
        public AptifriedSecurityKeyMap()
        {
            Table("vwSecurityKeys");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.KeyValue);
        }
    }
}