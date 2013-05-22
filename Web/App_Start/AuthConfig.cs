using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "000000004C0E9729",
            //    clientSecret: "SdfclQdBhKYKkFUX6-qrkZM-guaU-FDj");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterLinkedInClient(
            //    appId: "",
            //    appSecret: "");


            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
