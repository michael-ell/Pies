using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Codell.Pies.Web.Routing;
using Moq;

namespace Codell.Pies.Testing.BDD
{
    public abstract class ControllerContextBase<TController> : ContextBase<TController> where TController : Controller
    {
        protected Mock<HttpContextBase> MockHttpContext { get; private set; }
        protected Mock<HttpRequestBase> MockRequest { get; private set; }
        protected Mock<HttpResponseBase> MockResponse { get; private set; }

        protected override TController CreateSut()
        {
            var sut = base.CreateSut();
            MockHttpContext = MockFor<HttpContextBase>();
            MockRequest = MockFor<HttpRequestBase>();
            MockResponse = MockFor<HttpResponseBase>();

            MockHttpContext.Setup(context => context.Request).Returns(MockRequest.Object);
            MockHttpContext.Setup(context => context.Response).Returns(MockResponse.Object);

            var requestContext = new RequestContext(MockHttpContext.Object, new RouteData());
            sut.ControllerContext = new ControllerContext(MockHttpContext.Object, new RouteData(), sut);

            var routes = new RouteCollection();
            RouteBuilder.Register(routes);
            sut.Url = new UrlHelper(requestContext, routes);
            return sut;
        }
    }
}