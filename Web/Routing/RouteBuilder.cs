using System.Web.Mvc;
using System.Web.Routing;
using Codell.Pies.Web.Controllers;

namespace Codell.Pies.Web.Routing
{
    public class RouteBuilder
    {
        public static void Register(RouteCollection routes)
        {
            var builder = new RouteBuilder();
            builder.Build(routes);
        }

        public void Build(RouteCollection routes)
        {
            routes.MapRoute("", "home/page/{page}", new { controller = "Home", action = "Index" });
            routes.MapRoute("", "home/find/{tag}", new { controller = "Home", action = "Find" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { typeof(HomeController).Namespace });
        }         
    }
}