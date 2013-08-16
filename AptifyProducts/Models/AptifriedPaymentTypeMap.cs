#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedPaymentTypeMap : ClassMap<AptifriedPaymentType>
    {
        public AptifriedPaymentTypeMap()
        {
            Table("vwPaymentTypes");
            Id(x => x.Id);
            Map(x => x.Active);
            Map(x => x.Inflow);
            Map(x => x.Name);
            Map(x => x.Type);
        }
    }
}