#region using

using System;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedWebNotification
    {
        public virtual int Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual bool Seen { get; set; }
        public virtual int PersonId { get; set; }
    }
}