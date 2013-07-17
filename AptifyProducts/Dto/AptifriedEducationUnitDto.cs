#region using

using System;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedEducationUnitDto
    {
        public int Id { get; set; }
        public AptifriedPersonDto Person { get; set; }
        public DateTime DateEarned { get; set; }
        public DateTime DateExpires { get; set; }
        public DateTime SelectDate { get; set; }
        public AptifriedEducationCategoryDto EducationCategory { get; set; }

        public string Status { get; set; }
        public decimal EducationUnits { get; set; }

        public string Source { get; set; }
        public string ExternalSource { get; set; }
        public string ExternalSourceDescription { get; set; }
        public bool ExternalSourceVerified { get; set; }
        public string ExternalCPECity { get; set; }
        public string ExternalCPESponsor { get; set; }
        public string ExternalCPEInstructor { get; set; }

        public AptifriedMeetingDto Meeting { get; set; }
        public AptifriedCpeCreditAdjustmentTypeDto CpeCreditAdjustmentType { get; set; }
        public bool Deactivate { get; set; }
    }
}