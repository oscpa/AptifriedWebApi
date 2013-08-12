#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Meeting
{
    public class AptifriedMeetingEductionUnitsMap : ClassMap<AptifriedMeetingEductionUnits>
    {
        public AptifriedMeetingEductionUnitsMap()
        {
            Table("vwMeetingEducationUnits");
            Id(x => x.Id);
            Map(x => x.EducationUnits);
            Map(x => x.CeType);
            Map(x => x.MeetingId);
            References(x => x.Category).Column("EducationCategoryID");
            ReadOnly();
        }
    }
}