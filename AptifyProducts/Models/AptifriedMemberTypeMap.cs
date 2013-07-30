using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models
{
    public class AptifriedMemberTypeMap : ClassMap<AptifriedMemberType>
    {
        public AptifriedMemberTypeMap()
        {
            Table("vwMemberTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.IsMember);
        }
    }
}