using System;
using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Web.Models.MyPies;
using Codell.Pies.Web.Models.Providers;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.Controllers
{
    [Authorize]
    public class MyPiesController : Controller
    {
        private readonly IPiesProvider _piesProvider;

        public MyPiesController(IPiesProvider piesProvider)
        {
            Verify.NotNull(piesProvider, "piesProvider");                      
            _piesProvider = piesProvider;

        }

        [HttpGet]
        public ActionResult Index(IPiesIdentity identity)
        {
            return View(new IndexModel { Owner = identity.User, Pies = _piesProvider.GetPiesFor(identity) });  
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View(_piesProvider.Get(id));
        }
    }
}