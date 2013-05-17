using System.Web;
using System.Web.Routing;
using Codell.Pies.Web.Routing;
using Moq;

namespace Codell.Pies.Tests.Web.Routing
{
    public class RoutingTesterBase
    {
        protected Mock<HttpContextBase> MockHttpContext { get; private set; }
        protected Mock<HttpRequestBase> MockHttpRequest { get; private set; }
        protected RouteCollection Routes { get; private set; }

        protected RoutingTesterBase()
        {
            InitializeMockObjects();
        }

        private void InitializeMockObjects()
        {
            MockHttpContext = new Mock<HttpContextBase>();
            MockHttpRequest = new Mock<HttpRequestBase>();
        }

        protected void MapRoutes()
        {
            Routes = RouteTable.Routes;
            Routes.Clear();
            RouteBuilder.Register(Routes);
        }
    }
}