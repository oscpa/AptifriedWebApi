using System.Data.Entity;
using Aptify.Framework.Application;

namespace AptifyWebApi.Models.Shared
{
    public class AptifyEntityContext : DbContext
    {
        public AptifyEntityContext()
            : base("AptifyEntities")
        {
        }

        /*
        public DbSet<vwAttachment> Attachments { get; set; }
        public DbSet<vwEducationCategory> EducationCategories { get; set; }
        public DbSet<vwEducationLevel> EducationLevels { get; set; }
        public DbSet<vwMeeting> Meetings { get; set; }
        public DbSet<vwMeetingType> MeetingTypes { get; set; }
        public DbSet<vwEducationUnit> EducationUnits { get; set; }
        public DbSet<vwProduct> Products { get; set; }
        public DbSet<vwAddress> Addresses { get; set; }
        */
    }
}