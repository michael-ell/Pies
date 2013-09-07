using System.Web.Mvc;

namespace Codell.Pies.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
    }
}