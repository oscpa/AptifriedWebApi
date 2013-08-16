#region using

using System.Security.Principal;

#endregion

namespace AptifyWebApi.Membership
{
    public class AptifriedIdentity : IIdentity
    {
        private readonly string authenticationType;
        private readonly bool isAuthticated;
        private readonly string name;

        public AptifriedIdentity(bool isAuthenticated, string name)
        {
            authenticationType = "Aptifried";
            isAuthticated = isAuthenticated;
            this.name = name;
        }

        public string AuthenticationType
        {
            get { return authenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthticated; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}