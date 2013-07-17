#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedWebNotificationMap : ClassMap<AptifriedWebNotification>
    {
        public AptifriedWebNotificationMap()
        {
            Table("vwWebNotifications");
            Id(x => x.Id);
            Map(x => x.DateCreated);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Seen);
            Map(x => x.PersonId).Column("PersonID");
        }
    }
}