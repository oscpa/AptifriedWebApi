using AptifyWebApi.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AptifyWebApi.Membership {
    public class AptifriedPrincipal : IPrincipal {

        public AptifriedPrincipal(AptifriedIdentity identity) {
            this.identity = identity;
        }

        private AptifriedIdentity identity;

        public IIdentity Identity {
            get { return identity; }
        }

        public bool IsInRole(string role) {
            return AptifriedAuthorizationFactory.IsUserInRole(this.identity.Name, role);
        }
    }
}