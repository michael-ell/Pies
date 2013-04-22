﻿using System.Web.Mvc;
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
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Pie", action = "Index", id = UrlParameter.Optional });
        }         
    }
}