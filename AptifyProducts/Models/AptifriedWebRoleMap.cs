using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebRoleMap : ClassMap<AptifriedWebRole> {
        public AptifriedWebRoleMap() {
            Table("vwWebGroupsWithCalculatedGroupsAndUniqueIDs");
            
            Id(x => x.UniqueId).Column("UniqueID");
            Map(x => x.Name).Column("Name");
            ReadOnly();
        }
    }
}