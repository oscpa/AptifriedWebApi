﻿using Aptify.Framework.DataServices;
using AptifyWebApi.Membership;
using AptifyWebApi.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AptifyWebApi.Factories {
    public class AptifriedAuthorizationFactory {

        private const string HTTP_HEADER_KEY = "X-OscpaStoreAuth-Token";
        private const string REQUEST_KEY = "oscpastoreauthtoken";

        private ISession session;

        public AptifriedAuthorizationFactory() : this(DependencyResolver.Current.GetService<ISession>()) {}
        public AptifriedAuthorizationFactory(ISession session) {
            this.session = session;
        }

        internal static bool TryGetAuthorization(HttpRequestMessage requestMessage , out AptifriedPrincipal principal) {
            string authToken = string.Empty;
            IEnumerable<string> valueResult;
            if (requestMessage.Headers.TryGetValues(HTTP_HEADER_KEY,out valueResult )){
                authToken = valueResult.SingleOrDefault();
            } else {
                authToken = requestMessage.RequestUri.ParseQueryString()[REQUEST_KEY];              
            }

            return TryGetAuthorizationInternal(authToken, out principal);
       
        }

        internal static bool TryGetAuthorization(HttpRequest request, out AptifriedPrincipal principal) {
            string authToken = request.Headers[HTTP_HEADER_KEY];
            if (string.IsNullOrEmpty(authToken)) {
                authToken = request[REQUEST_KEY];
            }
            
            return TryGetAuthorizationInternal(authToken, out principal);

        }

        private static bool TryGetAuthorizationInternal(string authToken, out AptifriedPrincipal principal) {
            bool authorizationFound = false;
            principal = null;

            if (!string.IsNullOrEmpty(authToken)) {
                ISession session = DependencyResolver.Current.GetService<ISession>();
                var foundUser = session.QueryOver<AptifriedWebUser>()
                    .Where(u => u.UniqueId == authToken)
                    .SingleOrDefault();

                if (foundUser != null) {
                    authorizationFound = true;

                    principal = new AptifriedPrincipal(
                        new AptifriedIdentity(true, foundUser.UserName.Trim()));
                }
            }
            return authorizationFound;
        }

        internal static UserCredentials GetUserCredientails() {
            UserCredentials uc = new Aptify.Framework.DataServices.UserCredentials(
                 ValidateAppSetting("AptifyDBServer", true),
                 ValidateAppSetting("AptifyUsersDB", true),
                 ValidateAppSetting("AptifyEntitiesDB", true),
                 Convert.ToBoolean(ValidateAppSetting("AptifyEBusinessSQLIsTrusted", true)),
                 Convert.ToInt64(ValidateAppSetting("AptifyEBusinessWebEmployeeID", false)),
                 ValidateAppSetting("AptifyEBusinessSQLLogin", false),
                 ValidateAppSetting("AptifyEBusinessSQLPWD", false),
                 null,
                 false,
                 -1,
                 false);

            return uc;
        }

        private static string ValidateAppSetting(string keyName, bool requre = false) {
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[keyName]) && requre)
                throw new ConfigurationErrorsException("Missing Key: " + keyName);
            else
                return System.Configuration.ConfigurationManager.AppSettings[keyName];

        }

        internal static bool IsUserInRole(string userName, string roleName) {
            return true;
        }
    }
}