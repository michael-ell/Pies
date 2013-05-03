using System.Web;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Codell.Pies.Web.Security
{
    public interface IOpenIdGateway
    {
        IAuthenticationRequest CreateRequest(string identifier);
        OpenIdUser GetUser();
        HttpCookie CreateFormsAuthenticationCookie(OpenIdUser user);
    }
}