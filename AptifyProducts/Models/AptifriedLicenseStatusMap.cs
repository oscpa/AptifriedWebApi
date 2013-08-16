#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedLicenseStatusMap : ClassMap<AptifriedLicenseStatus>
    {
        public AptifriedLicenseStatusMap()
        {
            Table("vwLicenseStatus");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.OldId);
            ReadOnly();
        }
    }
}