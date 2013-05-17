using System.Web;
using System.Web.Routing;
using FluentAssertions;

namespace Codell.Pies.Tests.Web.Routing
{
    public class OutboundTester : RoutingTesterBase
    {
        private readonly object _routeValues;
        private string _generatedUrl;

        public OutboundTester(object routeValues)
        {
            _routeValues = routeValues;
        }

        public void Generates(string expectedUrl)
        {
            MapRoutes();
            SimulateUrlGeneration();
            Assert(expectedUrl);
        }

        private void SimulateUrlGeneration()
        {
            MockHttpContext.Setup(context => context.Request).Returns(MockHttpRequest.Object);
            MockHttpContext.Setup(context => context.Response).Returns(new StubResponse());
            MockHttpRequest.Setup(request => request.ApplicationPath).Returns("/");

            var ctx = new RequestContext(MockHttpContext.Object, new RouteData());
            _generatedUrl = Routes.GetVirtualPath(ctx, new RouteValueDictionary(_routeValues)).VirtualPath;
        }

        private void Assert(string expectedUrl)
        {
            _generatedUrl.ToLower().Should().Be(expectedUrl.ToLower());
        }

        private class StubResponse : HttpResponseBase
        {
            //Called for cookieless sessions.  Don't care for tests, so just return path...
            public override string ApplyAppPathModifier(string virtualPath)
            {
                return virtualPath;
            }
        }
    }
}