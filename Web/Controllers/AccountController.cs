using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Codell.Pies.Web.Security;
using Common.Logging;
using LinqToTwitter;
using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

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
                //var context = new TwitterContext(new MvcAuthorizer());
                //var user = context.User.SingleOrDefault(u => u.Type == UserType.Show && u.ScreenName == result.UserName);
                //try
                //{
                //    using (var wc = new WebClient())
                //    {
                //        string json;
                //        wc.Headers.Add("OAuth", " ");
                //        wc.Headers.Add("oauth_consumer_key", "wMqGbIeeaXxYCHHcXa7CzA");
                //        wc.Headers.Add("oauth_nonce", "");
                //        wc.Headers.Add("oauth_signature_method", "HMAC-SHA1");
                //        wc.Headers.Add("oauth_timestamp", DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString());
                //        wc.Headers.Add("oauth_token", result.ExtraData["token"]);
                //        wc.Headers.Add("oauth_version", "1.0");
                //        wc.Headers.Add("oauth_signature", Sign("4lttnxAhJznWujuAfQSz7MVHun6Eq3hd4gJeiYZ9KU4", result.ExtraData["token"]));
                //        using (var reader = new StreamReader(wc.OpenRead(string.Format("https://api.twitter.com/1.1/users/show.json?screen_name={0}", result.UserName))))
                //        {
                //            json = reader.ReadToEnd();
                //        }

                //    }
                //}
                //catch (Exception e)
                //{
                //    LogManager.GetCurrentClassLogger().Error(e); 
                //}
                HttpContext.SetUser(result);
                return RedirectToLocal(returnUrl);
            }
            return RedirectToAction("ExternalLoginFailure");
        }

        //private string Sign(string consumerSecret, string accessTokenSecret, string data)
        //{
        //    var combinedSecrets =
        //        HttpUtility.UrlEncode(consumerSecret) +
        //        "&" +
        //        HttpUtility.UrlEncode(accessTokenSecret);
        //    var hashAlgorithm = new HMACSHA1(Encoding.UTF8.GetBytes(combinedSecrets));

        //    var dataBuffer = Encoding.UTF8.GetBytes(data);
        //    var hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

        //    return Convert.ToBase64String(hashBytes);
        //}

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View("~/Views/Error/Default.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.ClearUser();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
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