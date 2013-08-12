#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedZipCodeMap : ClassMap<AptifriedZipCode>
    {
        public AptifriedZipCodeMap()
        {
            Table("vwZipCodes");
            Id(x => x.Id).Column("ID");
            Map(x => x.Longitude).Column("long");
            Map(x => x.Latitude).Column("lat");
            Map(x => x.PostalCode);
            Map(x => x.CountryCodeId);
            ReadOnly();
        }
    }
}