#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedAttendeeStatusMap : ClassMap<AptifriedAttendeeStatus>
    {
        public AptifriedAttendeeStatusMap()
        {
            Table("vwAttendeeStatus");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}