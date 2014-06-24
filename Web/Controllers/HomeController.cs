using System;
using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Web.Attributes;
using Codell.Pies.Web.Models.Home;
using Codell.Pies.Web.Models.Providers;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Controllers
{
    [MobileRedirect]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IPiesProvider _piesProvider;

        public HomeController(IPiesProvider piesProvider)
        {
            Verify.NotNull(piesProvider, "piesProvider");            
            _piesProvider = piesProvider;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var result = _piesProvider.GetPage(page);
            return View(new IndexModel
                            {
                                Pies = result.Pies,
                                Paging = new PagingModel{CurrentPage = result.CurrentPage, TotalPages = result.TotalPages}
                            });
        }

        [HttpGet]
        public ActionResult Share(Guid id)
        {
            return View("Index", new IndexModel { Pies = new[] { _piesProvider.Get(id) } });
        }
    }
}