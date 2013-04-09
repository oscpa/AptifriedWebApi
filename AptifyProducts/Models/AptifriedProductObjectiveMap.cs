using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedProductObjectiveMap : ClassMap<AptifriedProductObjective> {
        public AptifriedProductObjectiveMap() {
            Table("vwOSCPAMarketingObjectives");
            Id(x => x.Id);
            Map(x => x.ProductId);
            Map(x => x.Sequence);
            Map(x => x.Objective);
        }
    }
}