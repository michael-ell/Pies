using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public HomeController(IRepository repository, IMappingEngine mapper)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
            
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Redirect(Url.Content("~/areas/sencha/index.html"));
        }

        [HttpGet]
        public JsonResult GetRecent()
        {
            return ToJsonResult(_repository.Find<Pie>(pie => pie.IsEmpty == false).OrderByDescending(pie => pie.CreatedOn).Take(12));
        }

        private JsonResult ToJsonResult(IEnumerable<Pie> found)
        {
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return new JsonResult{Data = pies, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}
