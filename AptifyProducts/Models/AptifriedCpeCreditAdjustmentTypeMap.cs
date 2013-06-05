using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedCpeCreditAdjustmentTypeMap : ClassMap<AptifriedCpeCreditAdjustmentType> {
        public AptifriedCpeCreditAdjustmentTypeMap() {
            Table("vwCPECreditAdjustmentTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            ReadOnly();
        }
    }
}