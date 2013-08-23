using System.Web.Mvc;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
