#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedProductObjectiveMap : ClassMap<AptifriedProductObjective>
    {
        public AptifriedProductObjectiveMap()
        {
            Table("vwOSCPAMarketingObjectives");
            Id(x => x.Id);
            Map(x => x.ProductId);
            Map(x => x.Sequence);
            Map(x => x.Objective);
        }
    }
}