#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedAuthroizedUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public int PersonId { get; set; }
        public virtual IList<AptifriedAuthorizedRoleDto> Roles { get; set; }
    }
}