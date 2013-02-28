using System.Web.Routing;

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
        }         
    }
}