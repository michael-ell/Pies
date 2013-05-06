using System.Web;
using System.Web.Mvc;
using Codell.Pies.Web.Extensions;

namespace Codell.Pies.Web.Security
{
    public class OpenIdAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            httpContext.SetIdentity();
            return isAuthorized;
        }         
    }
}