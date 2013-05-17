namespace Codell.Pies.Tests.Web.Routing
{
    public class Verify
    {
        public static InboundTester Url(string url)
        {
            return new InboundTester(url);
        }

        public static OutboundTester RouteData(object routeValues)
        {
            return new OutboundTester(routeValues);
        }
    }
}