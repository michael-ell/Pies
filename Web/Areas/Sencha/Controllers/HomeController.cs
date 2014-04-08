using System;
using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ISettings _settings;

        public HomeController(ISettings settings)
        {
            Verify.NotNull(settings, "settings");
                        
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            return Redirect(string.Format("{0}{1}", Url.Content(_settings.Get<string>(Keys.MobilePage)), id.IsEmpty() ? "" : "?share=" + id));
        }

        [HttpGet]
        public ActionResult Share(Guid id)
        {
            return Index(id.ToString());
        }
    }
}
