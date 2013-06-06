using System.Web.Mvc;

namespace Codell.Pies.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Attributes.HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}