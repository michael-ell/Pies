using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;

namespace Codell.Pies.Tests.Web.Routing
{
    public class InboundTester : RoutingTesterBase
    {
        private readonly string _url;
        private RouteData _returnedRouteData;

        public InboundTester(string url)
        {
            _url = url;       
        }

        public void MapsTo(object expectedValues)
        {            
            MapRoutes();
            SimulateUrlRequest();
            Assert(expectedValues);
        }

        private void SimulateUrlRequest()
        {
            MockHttpContext.Setup(context => context.Request).Returns(MockHttpRequest.Object);
            MockHttpRequest.Setup(request => request.AppRelativeCurrentExecutionFilePath).Returns(_url);
            _returnedRouteData = Routes.GetRouteData(MockHttpContext.Object);
            InvokeCustomRouteHandler();
        }

        private void InvokeCustomRouteHandler()
        {
            if (_returnedRouteData.RouteHandler.GetType() == typeof (MvcRouteHandler)) return;

            var requestContext = new RequestContext(MockHttpContext.Object, _returnedRouteData);
            _returnedRouteData.RouteHandler.GetHttpHandler(requestContext);
        }

        private void Assert(object expectedValues)
        {
            _returnedRouteData.Should().NotBeNull();
            var expectedDictionary = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDictionary)
            {
                if (expectedVal.Value == null)
                {
                    _returnedRouteData.Values[expectedVal.Key].Should().BeNull();
                }
                else
                {
                    expectedVal.Value.ToString().Equals(_returnedRouteData.Values[expectedVal.Key].ToString(), StringComparison.OrdinalIgnoreCase)
                                                .Should()
                                                .BeTrue();
                }
            }
        }

        public InboundTester Posting(params KeyValuePair<string, string>[] pairing)
        {
            foreach (var pair in pairing)
            {
                MockHttpRequest.Setup(request => request[pair.Key]).Returns(pair.Value);
            }
            return this;
        }
    }
}