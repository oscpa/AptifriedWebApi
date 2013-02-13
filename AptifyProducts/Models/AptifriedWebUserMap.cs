using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebUserMap : ClassMap<AptifriedWebUser> {
        public AptifriedWebUserMap() {
            Table("vwWebUsers");
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.LinkId);
            Map(x => x.LinkType);
            Map(x => x.PersonId).Column("LinkID");
            Map(x => x.UserName).Column("UserID");
            Map(x => x.UniqueId).Column("CmsGuid");
            Map(x => x.EncryptedPassword).Column("PWD");

            HasManyToMany(x => x.Roles)
                .AsBag()
                .Table("vwJoinWebGroupsCalculatedWebUsers")
                .ParentKeyColumn("WebUserID")
                .ChildKeyColumn("WebGroupUniqueID");

            ReadOnly();
        }
    }
}