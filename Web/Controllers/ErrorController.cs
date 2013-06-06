using System.Web.Mvc;

namespace Codell.Pies.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
    }
}