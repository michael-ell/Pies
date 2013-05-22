using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Web.Security;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.Messaging;
using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IOpenIdGateway _gateway;

        public AccountController(IOpenIdGateway gateway)
        {
            Verify.NotNull(gateway, "gateway");            
            _gateway = gateway;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            //var user = _gateway.GetUser();
            //if (user != null)
            //{
            //    var cookie = _gateway.CreateFormsAuthenticationCookie(user);
            //    HttpContext.Response.Cookies.Add(cookie);

            //    return new RedirectResult(Request.Params["ReturnUrl"] ?? "/");
            //}
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Login(string openid_identifier)
        //{
        //    var response = _gateway.CreateRequest(openid_identifier);
        //    return response != null ? response.RedirectingResponse.AsActionResult() : View();
        //}



        [HttpGet]
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLogin", OAuthWebSecurity.RegisteredClientData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (result.IsSuccessful)
            {

                return RedirectToLocal(returnUrl);
            }
            return RedirectToAction("ExternalLoginFailure");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Pie");
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }

            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

    }
}