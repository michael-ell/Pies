using System.Web.Mvc;
using Elmah;

namespace Codell.Pies.Web.Attributes
{
    public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ErrorSignal.FromCurrentContext().Raise(filterContext.Exception); 
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(500);
                filterContext.ExceptionHandled = true;
            }
            else
            {
                View = "~/Views/Error/Default.cshtml";         
                base.OnException(filterContext);
            }
        }
    }
}