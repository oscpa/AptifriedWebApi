#region using

using System;
using AptifyWebApi.Models.Meeting;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedEducationUnit
    {
        public virtual int Id { get; set; }
        public virtual AptifriedPerson Person { get; set; }
        public virtual DateTime DateEarned { get; set; }
        public virtual DateTime DateExpires { get; set; }
        public virtual DateTime SelectDate { get; set; }
        public virtual AptifriedEducationCategory EducationCategory { get; set; }

        public virtual string Status { get; set; }
        public virtual decimal EducationUnits { get; set; }

        public virtual string Source { get; set; }
        public virtual string ExternalSource { get; set; }
        public virtual string ExternalSourceDescription { get; set; }
        public virtual bool ExternalSourceVerified { get; set; }
        public virtual string ExternalCPECity { get; set; }
        public virtual string ExternalCPESponsor { get; set; }
        public virtual string ExternalCPEInstructor { get; set; }

        public virtual AptifriedMeeting Meeting { get; set; }
        public virtual AptifriedCpeCreditAdjustmentType CpeCreditAdjustmentType { get; set; }
        public virtual bool Deactivate { get; set; }
    }
}