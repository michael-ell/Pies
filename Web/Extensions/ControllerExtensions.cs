using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Codell.Pies.Common;

namespace Codell.Pies.Web.Extensions
{
    public static class ControllerExtensions
    {
         public static string Render(this Controller controller, string viewName, object model)
         {
             Verify.NotNull(controller, "controller");
             Verify.NotWhitespace(viewName, "viewName");
             Verify.NotNull(model, "model");             

             var routeData = new RouteData();
             routeData.Values.Add("controller", controller.GetType().Name.Replace("Controller", ""));
             using (var sw = new StringWriter())
             {
                 controller.ControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new SimpleWorkerRequest("", "", sw))), routeData, controller);
                 var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                 var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, new ViewDataDictionary { Model = model }, new TempDataDictionary(), sw);
                 viewResult.View.Render(viewContext, sw);
                 return sw.GetStringBuilder().ToString();
             }             
         }
    }
}