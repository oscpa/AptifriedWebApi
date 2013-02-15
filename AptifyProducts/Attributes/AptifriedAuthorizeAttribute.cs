using Microsoft.Practices.Unity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Threading;
using AptifyWebApi.Models;

namespace AptifyWebApi.Attributes {
    public class AptifriedAuthorizeAttribute : System.Web.Http.AuthorizeAttribute{


        private const string HEADER_KEY = "";
        private ISession session;

        public AptifriedAuthorizeAttribute() : this(DependencyResolver.Current.GetService<ISession>()) { }
        public AptifriedAuthorizeAttribute(ISession session) {
            this.session = session;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext) {
            return base.IsAuthorized(actionContext);
        }
        public override void OnAuthorization(HttpActionContext actionContext) {
            base.OnAuthorization(actionContext);
        }
    }
}