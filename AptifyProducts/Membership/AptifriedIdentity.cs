using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AptifyWebApi.Membership {
    public class AptifriedIdentity : IIdentity {

        public AptifriedIdentity(bool isAuthenticated, string name) {
            this.authenticationType = "Aptifried";
            this.isAuthticated = isAuthenticated;
            this.name = name;
        }

        private string authenticationType;
        private bool isAuthticated;
        private string name;

        public string AuthenticationType {
            get { return authenticationType; }
        }

        public bool IsAuthenticated {
            get { return isAuthticated; }
        }

        public string Name {
            get { return name; }
        }
    }
}