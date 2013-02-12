using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebRoleMap : ClassMap<AptifriedWebRole> {
        public AptifriedWebRoleMap() {
            Table("vwWebGroupsWithCalculatedGroupsAndUniqueIDs");
            Id(x => x.UniqueId);
            Map(x => x.Name);
        }
    }
}