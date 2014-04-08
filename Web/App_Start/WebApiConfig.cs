using System.Linq;
using System.Web.Http;

namespace Codell.Pies.Web.App_Start
{
    public static class WebApiConfig
    {
        public static class RouteNames
        {
            public const string Default = "DefaultApi";
            public const string Find = "FindApi";
        }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(RouteNames.Find, "api/home/find/{tag}", new { Area = "Api", Controller = "Home", Action = "Find", tag = RouteParameter.Optional });
            config.Routes.MapHttpRoute(RouteNames.Default, "api/{controller}/{action}/{id}", new { Area = "Api", id = RouteParameter.Optional });  
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
