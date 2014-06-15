using System;
using System.IO;
using System.Net;
using System.Web;
using Codell.Pies.Web.Security;
using Common.Logging;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using Microsoft.Web.Helpers;
using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.App_Start
{
    public static class AuthConfig
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void RegisterAuth()
        {
            try
            {
                var configuration = new AuthenticationClientConfiguration();
                OAuthWebSecurity.RegisterGoogleClient();
                OAuthWebSecurity.RegisterMicrosoftClient(configuration.Microsoft.Key, configuration.Microsoft.Secret);
                OAuthWebSecurity.RegisterTwitterClient(configuration.Twitter.Key, configuration.Twitter.Secret);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterLinkedInClient(consumerKey: "xommx7yio9ei", consumerSecret: "d9ATsd2vitInuWvB");
        }
    }
}
