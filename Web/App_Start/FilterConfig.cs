using System.Web.Mvc;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OpenIdAuthorizeAttribute());
        }
    }
}