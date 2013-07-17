#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMemberStatusTypeMap : ClassMap<AptifriedMemberStatusType>
    {
        public AptifriedMemberStatusTypeMap()
        {
            Table("vwMemberStatusTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.OldID);
            Map(x => x.IsActive);
            Map(x => x.IsBenefitEligible);
            Map(x => x.DefaultMemberType);
            Map(x => x.DefaultType);
            Map(x => x.IsMember);
            Map(x => x.UniqueID);
        }
    }
}