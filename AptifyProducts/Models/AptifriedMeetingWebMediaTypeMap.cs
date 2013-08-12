#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingWebMediaTypeMap : ClassMap<AptifriedMeetingWebMediaType>
    {
        public AptifriedMeetingWebMediaTypeMap()
        {
            Table("vwWebMediaTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}