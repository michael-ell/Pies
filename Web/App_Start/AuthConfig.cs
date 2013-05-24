using Microsoft.Web.WebPages.OAuth;

namespace Codell.Pies.Web.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            OAuthWebSecurity.RegisterGoogleClient();
            OAuthWebSecurity.RegisterMicrosoftClient(clientId: "000000004C0E9729", clientSecret: "SdfclQdBhKYKkFUX6-qrkZM-guaU-FDj");
            OAuthWebSecurity.RegisterTwitterClient(consumerKey: "wMqGbIeeaXxYCHHcXa7CzA", consumerSecret: "4lttnxAhJznWujuAfQSz7MVHun6Eq3hd4gJeiYZ9KU4");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterLinkedInClient(consumerKey: "xommx7yio9ei", consumerSecret: "d9ATsd2vitInuWvB");
        }
    }
}
