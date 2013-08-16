#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedAddressMap : ClassMap<AptifriedAddress>
    {
        public AptifriedAddressMap()
        {
            Table("vwAddressesTiny");
            Id(x => x.Id);
            Map(x => x.Line1);
            Map(x => x.Line2);
            Map(x => x.Line3);
            Map(x => x.City);
            Map(x => x.StateProvince);
            Map(x => x.PostalCode);
        }
    }
}