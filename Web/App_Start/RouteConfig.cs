using System.Web.Mvc;
using System.Web.Routing;
using Codell.Pies.Web.Routing;

namespace Codell.Pies.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapHubs();
            RouteBuilder.Register(routes);
        }
    }
}