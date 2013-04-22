using System.Web.Mvc;
using Codell.Pies.Web.Extensions;

namespace Codell.Pies.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
         public JsonResult JsonResult(object data)
         {
             return Json(data.ToJson(), JsonRequestBehavior.AllowGet);
         }
    }
}