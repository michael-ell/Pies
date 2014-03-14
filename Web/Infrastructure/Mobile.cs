using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Codell.Pies.Common;
using WURFL;
using WURFL.Config;

namespace Codell.Pies.Web.Infrastructure
{
    public static class Mobile
    {
         public static class Detection
         {
             private const String WurflManagerCacheKey = "__WurflManager";

             public static IWURFLManager Initialize(HttpContext context)
             {
                 return Initialize(new HttpContextWrapper(context));
             }

             public static IWURFLManager Initialize( HttpContextBase context)
             {
                 //Could move to app config but ApplicationConfigurer seems to have some issues...
                 var configurer = new InMemoryConfigurer().MainFile(HttpContext.Current.Server.MapPath("~/App_Data/wurfl.zip")).SetMatchMode(MatchMode.Performance);
                 var manager = WURFLManagerBuilder.Build(configurer);
                 HttpContext.Current.Cache[WurflManagerCacheKey] = manager;
                 return manager;
             }

             public static bool IsMobile(HttpContext context)
             {
                 return context != null && IsMobile(new HttpContextWrapper(context));
             }

             public static bool IsMobile(HttpContextBase context)
             {
                 return CurrentCapabilityFor(context, "is_wireless_device") && IsNotTablet(context);
             }

             private static bool IsNotTablet(HttpContextBase context)
             {
                 return !IsTablet(context);
             }

             private static bool IsTablet(HttpContextBase context)
             {
                 return CurrentCapabilityFor(context, "is_tablet");
             }

             private static bool CurrentCapabilityFor(HttpContextBase context, string capability)
             {
                 if (context == null || capability.IsEmpty()) return false;

                 var manager = context.Cache[WurflManagerCacheKey] as IWURFLManager ?? Initialize(context);
                 var value = manager.GetDeviceForRequest(context.Request.UserAgent).GetCapability(capability);
                 bool result;
                 return bool.TryParse(value, out result) && result;
             }
         }

        public static class Redirection
        {
            public static string UrlToFullSite()
            {
                return "mode/full";
            }

            public static RedirectToRouteResult RedirectResultFrom(HttpContextBase context, RouteData routeData)
            {
                if (Detection.IsMobile(context) && NotAlreadyInMobileSite(routeData) && HaveNotRequestedFullSite(routeData))
                {
                    return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home", area = "Sencha"}));
                }
                return null;
            }

            private static bool NotAlreadyInMobileSite(RouteData routeData)
            {
                return !HasMode(routeData, SiteMode.Mobile);
            }

            private static bool HaveNotRequestedFullSite(RouteData routeData)
            {
                return !HasMode(routeData, SiteMode.Full);
            }

            private static bool HasMode(RouteData routeData, SiteMode mode)
            {
                if (routeData == null) return false;

                object result;
                return routeData.Values.TryGetValue("mode", out result) && 
                       string.Equals(result.ToString(), mode.ToString(), StringComparison.OrdinalIgnoreCase);
            }

            public enum SiteMode
            {
                Full,
                Mobile
            }            
        }
    }
}