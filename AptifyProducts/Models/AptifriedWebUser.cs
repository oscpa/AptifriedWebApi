#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedWebUser
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual int PersonId { get; set; }
        public virtual string LinkType { get; set; }
        public virtual int LinkId { get; set; }
        public virtual string UniqueId { get; set; }
        public virtual string EncryptedPassword { get; set; }

        public virtual IList<AptifriedWebRole> Roles { get; set; }
        public virtual IList<AptifriedWebShoppingCart> ShoppingCarts { get; set; }
    }
}