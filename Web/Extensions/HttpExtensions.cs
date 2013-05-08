using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Codell.Pies.Common.Security;
using Codell.Pies.Web.Security;
using Codell.Pies.Common;

namespace Codell.Pies.Web.Extensions
{
    public static class HttpExtensions
    {
        public static IPiesIdentity GetIdentity(this HttpContextBase context)
        {
            if (context == null || context.Request == null) return new AnonymousIdentity();   

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return new AnonymousIdentity();

            var ticket = cookie.Value;
            if (ticket.IsEmpty()) return new AnonymousIdentity();

            var decryptedTicket = FormsAuthentication.Decrypt(ticket);
            if (decryptedTicket == null) return new AnonymousIdentity();

            return new Identity(new OpenIdUser(decryptedTicket.UserData));            
        }

        public static void SetIdentity(this HttpContextBase context)
         {      
             context.User = new GenericPrincipal(context.GetIdentity(), null);
         }
    }
}