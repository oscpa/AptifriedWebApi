﻿using System;
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

            //AptifriedPrincipal principal;

            //if (AptifriedAuthorizationFactory.TryGetAuthorization(request, out principal)) {

            //}

            

            //if (request.Headers.Authorization == null) {
            //    return base.SendAsync(request, cancellationToken);
            //}

            //var authHeader = request.Headers.Authorization;

            //if (authHeader == null) {
            //    return base.SendAsync(request, cancellationToken);
            //}

            //if (authHeader.Scheme != "Basic") {
            //    return base.SendAsync(request, cancellationToken);
            //}

            //var encodedUserPass = authHeader.Parameter.Trim();
            //var userPass = Encoding.ASCII.GetString(Convert.FromBase64String(encodedUserPass));
            //var parts = userPass.Split(":".ToCharArray());
            //var username = parts[0];
            //var password = parts[1];

            //if (!Membership.ValidateUser(username, password)) {
            //    return base.SendAsync(request, cancellationToken);
            //}

            //var identity = new GenericIdentity(username, "Basic");
            //string[] roles = Roles.Provider.GetRolesForUser(username);
            //var principal = new GenericPrincipal(identity, roles);
            //Thread.CurrentPrincipal = principal;
            //if (HttpContext.Current != null) {
            //    HttpContext.Current.User = principal;
            //}

            return base.SendAsync(request, cancellationToken);
        }
    }
}