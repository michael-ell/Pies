using DotNetOpenAuth.OpenId.RelyingParty;

namespace Codell.Pies.Web.Authentication
{
    public interface IOpenIdGateway
    {
        IAuthenticationRequest GetRequest(string openIdIdentifier);
        OpenIdUser GetUser();         
    }
}