using CvLocate.Website.Bl;
using CvLocate.Website.Bl.Common;
using CvLocate.Website.Bl.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Hosting;

namespace CvLocate.Website.Core
{
    public class AuthenticationHandler : DelegatingHandler, IDelegatingHandler
    {
        public AuthenticationHandler()
        {

        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (HttpContext.Current.Request.Headers["Authorization"] == "null")
                return base.SendAsync(request, cancellationToken);


            var authHeader = HttpContext.Current.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }


        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        private bool AuthenticateUser(string encodedCredentials)
        {
            var credentials = Decode(encodedCredentials);

            var credentialsArray = credentials.Split(':');
            var userEmail = credentialsArray[0];
            var sessionId = credentialsArray[1];

            return SessionManager.Instance.AuthenticateUser(userEmail, sessionId);
        }
        private string Decode(string encodingStr)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                return encoding.GetString(Convert.FromBase64String(encodingStr));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IDelegatingHandler
    {
    }
}
