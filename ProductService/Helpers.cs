using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ProductService
{
    public class Helpers
    {
        public static TKey GetKeyFromUri<TKey>(HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);

            string serviceRoot = urlHelper.CreateODataLink(
                request.ODataProperties().RouteName,
                request.GetPathHandler(),
                new List<ODataPathSegment>());
            var odataPath = request.GetPathHandler().Parse(
                serviceRoot, uri.LocalPath, request.GetRequestContainer());

            var keySegment = odataPath.Segments.OfType<KeySegment>().FirstOrDefault();
            if (keySegment == null)
            {
                throw new InvalidOperationException("The link does not contain a key.");
            }

            return (TKey)keySegment.Keys.First().Value;
        }
    }
}

namespace BasicAuthentication
{
    using System.Text;
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Threading;
    using System.Security.Principal;

    class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;

                var decodedAuthToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                string userName = decodedAuthToken.Split(':')[0];
                string password = decodedAuthToken.Split(':')[1];

                if (IsAuthorizedUser(userName, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(userName), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        private bool IsAuthorizedUser(string userName, string password)
        {
            return userName == "admin" && password == "admin";
        }
    }
}