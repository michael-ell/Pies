using System.Web.Mvc;
using Codell.Pies.Web.Areas.Sencha.Models;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public void Login(UserModel model)
        {
            HttpContext.SetUser(new User(model));
        }

        [HttpPost]
        public void LogOff()
        {
            HttpContext.ClearUser();
        }
    }
}