using Codell.Pies.Web.Security;
using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            var configuration = new AuthenticationClientConfiguration();
            OAuthWebSecurity.RegisterGoogleClient();      
            OAuthWebSecurity.RegisterMicrosoftClient(configuration.Microsoft.Key, configuration.Microsoft.Secret);
            OAuthWebSecurity.RegisterTwitterClient(configuration.Twitter.Key, configuration.Twitter.Secret);

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterLinkedInClient(consumerKey: "xommx7yio9ei", consumerSecret: "d9ATsd2vitInuWvB");
        }
    }
}
