using System.Web.Mvc;

namespace Codell.Pies.Web.Controllers
{
    public class AccountController : ControllerBase
    {
         [AllowAnonymous]
         public ActionResult Login(string returnUrl)
         {
             return View();
         }
    }
}