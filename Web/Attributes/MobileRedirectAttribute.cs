using System.Web.Mvc;

namespace Codell.Pies.Web.Attributes
{
    public class MobileRedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Result = Infrastructure.Mobile.Redirection.RedirectResultFrom(filterContext.HttpContext, filterContext.RouteData);
            base.OnActionExecuting(filterContext);
        }         
    }
}