using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedSecurityKeyMap : ClassMap<AptifriedSecurityKey> {
        public AptifriedSecurityKeyMap() {
            Table("vwSecurityKeys");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.KeyValue);
        }
    }
}