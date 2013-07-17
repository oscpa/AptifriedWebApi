#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedCpeCreditAdjustmentTypeMap : ClassMap<AptifriedCpeCreditAdjustmentType>
    {
        public AptifriedCpeCreditAdjustmentTypeMap()
        {
            Table("vwCPECreditAdjustmentTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            ReadOnly();
        }
    }
}