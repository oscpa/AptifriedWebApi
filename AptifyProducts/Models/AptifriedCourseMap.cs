#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedCourseMap : ClassMap<AptifriedCourse>
    {
        public AptifriedCourseMap()
        {
            Table("vwCourses");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}