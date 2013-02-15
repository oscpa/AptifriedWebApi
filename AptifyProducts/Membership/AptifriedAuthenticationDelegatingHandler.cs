using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AptifyWebApi.Factories;

namespace AptifyWebApi.Membership {
    public class AptifriedAuthenticationDelegatingHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            AptifriedPrincipal principal;
            if (AptifriedAuthorizationFactory.TryGetAuthorization(request, out principal)) {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null) {
                    HttpContext.Current.User = principal;
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}