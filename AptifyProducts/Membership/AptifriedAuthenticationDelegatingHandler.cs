#region using

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AptifyWebApi.Factories;
using AptifyWebApi.Repository;

#endregion

namespace AptifyWebApi.Membership
{
    public class AptifriedAuthenticationDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            AptifriedPrincipal principal;
            if (AptifriedAuthorizationFactory.TryGetAuthorization(request, out principal))
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}