using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Codell.Pies.Web.Security
{
    public class OpenIdAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {
                var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    var ticket = cookie.Value;
                    if (!string.IsNullOrWhiteSpace(ticket))
                    {
                        var decryptedTicket = FormsAuthentication.Decrypt(ticket);
                        if (decryptedTicket != null)
                        {
                            var user = new OpenIdUser(decryptedTicket.UserData);
                            httpContext.User = new GenericPrincipal(new Identity(user), null);
                        }
                    }
                }
            }
            return isAuthorized;
        }         
    }
}