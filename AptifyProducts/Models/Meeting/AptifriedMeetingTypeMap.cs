#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingTypeMap : ClassMap<AptifriedMeetingType>
    {
        public AptifriedMeetingTypeMap()
        {
            Table("vwMeetingTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            ReadOnly();
        }
    }
}