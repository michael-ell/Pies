using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Web.Security;
using DotNetOpenAuth.Messaging;

namespace Codell.Pies.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IOpenIdGateway _gateway;

        public AccountController(IOpenIdGateway gateway)
        {
            Verify.NotNull(gateway, "gateway");            
            _gateway = gateway;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var user = _gateway.GetUser();
            if (user != null)
            {
                var cookie = _gateway.CreateFormsAuthenticationCookie(user);
                HttpContext.Response.Cookies.Add(cookie);

                return new RedirectResult(Request.Params["ReturnUrl"] ?? "/");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string openid_identifier)
        {
            var response = _gateway.CreateRequest(openid_identifier);
            return response != null ? response.RedirectingResponse.AsActionResult() : View();
        }
    }
}