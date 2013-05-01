using System;
using System.Web;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Codell.Pies.Web.Authentication
{
    public class OpenIdGateway : IOpenIdGateway
    {
        private readonly OpenIdRelyingParty _openId;

        public OpenIdGateway()
        {
            _openId = new OpenIdRelyingParty();
        }

        public IAuthenticationRequest GetRequest(string openIdIdentifier)
        {
            var request = _openId.CreateRequest(Identifier.Parse(openIdIdentifier));
            var fields = new ClaimsRequest
            {
                Email = DemandLevel.Require,
                FullName = DemandLevel.Require,
                Nickname = DemandLevel.Require
            };
            request.AddExtension(fields);

            return request;
        }

        public OpenIdUser GetUser()
        {
            OpenIdUser user = null;
            var response = _openId.GetResponse();
            if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {
                user = ResponseIntoUser(response);
            }

            return user;
        }

        private OpenIdUser ResponseIntoUser(IAuthenticationResponse response)
        {
            OpenIdUser user = null;
            var claimResponseUntrusted = response.GetUntrustedExtension<ClaimsResponse>();
            var claimResponse = response.GetExtension<ClaimsResponse>();

            if (claimResponse != null)
            {
                user = new OpenIdUser(claimResponse, response.ClaimedIdentifier);
            }
            else if (claimResponseUntrusted != null)
            {
                user = new OpenIdUser(claimResponseUntrusted, response.ClaimedIdentifier);
            }

            return user;
        }

        public HttpCookie CreateFormsAuthenticationCookie(OpenIdUser user)
        {
            var ticket = new FormsAuthenticationTicket(1, user.Nickname, DateTime.Now, DateTime.Now.AddDays(7), true, user.ToString());
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            return cookie;
        }
    }
}