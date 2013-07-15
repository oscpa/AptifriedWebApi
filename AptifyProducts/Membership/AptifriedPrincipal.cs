#region using

using System.Security.Principal;
using AptifyWebApi.Factories;
using AptifyWebApi.Repository;

#endregion

namespace AptifyWebApi.Membership
{
    public class AptifriedPrincipal : IPrincipal
    {
        private readonly AptifriedIdentity identity;

        public AptifriedPrincipal(AptifriedIdentity identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            return AptifriedAuthorizationFactory.IsUserInRole(identity.Name, role);
        }
    }
}