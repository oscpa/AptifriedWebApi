using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace AptifyWebApi.Controllers {
    public class OscpaAuthController : ApiController {
        public string authBaker(string userName, string password) {
            // todo authenticate
            return string.Empty;
        }

        public HttpResponseMessage Get() {
            string username = "joelmusheno";
            const string secretKey = "This is a secret key known only to the parties exchanging the token!";
            // Read in the current UTC time
            string currentUTCtime = DateTime.UtcNow.ToString();

            // This time the content to sign is the identity token (the username) 
            // appended with the timestamp appended with the secret key
            string contentToSignString = username + currentUTCtime + secretKey;

            // Create the MD5 hasher and a UTF8Encoding class
            var hasher = new MD5CryptoServiceProvider();
            var encoder = new UTF8Encoding();

            // Create the hash
            byte[] contentToSignData = encoder.GetBytes(contentToSignString);
            byte[] signatureData = hasher.ComputeHash(contentToSignData);
            string signatureString = Convert.ToBase64String(signatureData);

 
            // Create the MD5 hasher and a UTF8Encoding class and compute the hash
            // ... same as before ...
            // Add the username, timestamp, and signature to the querystring
            string navUrl = string.Format("http://localhost:4000?Username={0}&Timestamp={1}&Signature={2}",
                                          System.Web.HttpUtility.UrlEncode(username),
                                          System.Web.HttpUtility.UrlEncode(currentUTCtime),
                                          System.Web.HttpUtility.UrlEncode(signatureString));

            var responseMessage = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMessage.Headers.Location = new Uri(navUrl);

            return responseMessage;

        }

        public string UnprotectedResouce() {
            return "Unprotected";
        }

        [Authorize]
        public string ProtectedResource() {
            return "Protected";
        }
    }
}
