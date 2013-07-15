#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedCompanyMap : ClassMap<AptifriedCompany>
    {
        public AptifriedCompanyMap()
        {
            Table("vwCompaniesTiny");
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Address).Column("AddressID");
        }
    }
}