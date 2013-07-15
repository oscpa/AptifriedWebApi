#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedWebRoleMap : ClassMap<AptifriedWebRole>
    {
        public AptifriedWebRoleMap()
        {
            Table("vwWebGroupsWithCalculatedGroupsAndUniqueIDs");

            Id(x => x.UniqueId).Column("UniqueID");
            Map(x => x.Name).Column("Name");
            ReadOnly();
        }
    }
}