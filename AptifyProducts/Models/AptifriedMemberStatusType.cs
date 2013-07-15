#region using

using System;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedMemberStatusType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string OldID { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsBenefitEligible { get; set; }
        public virtual string DefaultMemberType { get; set; }
        public virtual string DefaultType { get; set; }
        public virtual bool IsMember { get; set; }
        public virtual Guid UniqueID { get; set; }
    }
}