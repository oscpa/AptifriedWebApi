using AptifyWebApi.Models.Shared;

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedEducationCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Status { get; set; }

        public bool IsActive
        {
            get
            {
                return Status.ToLowerInvariant().Contains(EnumsAndConstantsToAvoidDatabaseChanges.EducationCategoryStatusActive);
            }
        }
    }
}