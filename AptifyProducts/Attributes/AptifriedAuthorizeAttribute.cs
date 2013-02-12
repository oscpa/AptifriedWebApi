using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AptifyWebApi.Attributes {
    public class AptifriedAuthorizeAttribute : AuthorizeAttribute {

        private ISession session;
        public AptifriedAuthorizeAttribute(ISession session) {
            this.session = session;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext) {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "AptifriedAuth");
            throw new HttpResponseException(challengeMessage);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext) {
            return Authorize(actionContext);
        }

        public override void OnAuthorization(HttpActionContext actionContext) {
            base.OnAuthorization(actionContext);
        }

        protected bool Authorize(HttpActionContext actionContext) {
            bool isAuthorized = false;

            if (actionContext.Request.Headers.Contains("X-User-Data")) {
                isAuthorized = true;
            } 

            return isAuthorized;
        }

    }
}