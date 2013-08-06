#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedMeetingTypeGroupMap : ClassMap<AptifriedMeetingTypeGroup>
    {
        public AptifriedMeetingTypeGroupMap()
        {
            Table("vwMeetingTypeGroups");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}