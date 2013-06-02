using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Home;
using Codell.Pies.Web.Models.Shared;
using Pie = Codell.Pies.Core.ReadModels.Pie;

namespace Codell.Pies.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : ControllerBase
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
        public ActionResult Index(int? page)
        {
            const int pageSize = 2;
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }
            var previous = (page.Value == 1 ? 0 : page.Value - 1) * pageSize;
            var pies = _repository.Get<Pie>().Where(pie => pie.IsEmpty == false)
                                             .Skip(previous)
                                             .Take(pageSize)
                                             .OrderByDescending(pie => pie.CreatedOn)
                                             .ToList();
            //var totalPages = _repository.Count<Pie>() / pageSize;
            var totalPages = _repository.Get<Pie>().Count(pie => pie.IsEmpty == false) / pageSize;
            return View(new IndexModel
                            {
                                Pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(pies),
                                Paging = new PagingModel{CurrentPage = page.Value, TotalPages = totalPages}
                            });
        }

        [HttpGet]
        public JsonResult GetRecent()
        {
            return ToJsonResult(_repository.Find<Pie>(pie => pie.IsEmpty == false).OrderByDescending(pie => pie.CreatedOn).Take(12));
        }


        [HttpGet]
        public JsonResult Find(string tag)
        {
            return tag.IsEmpty() ? GetRecent() : ToJsonResult(_repository.Find<Pie>(pie => pie.Tags.Contains<string>(new SearchableTag(tag))));
        }

        private JsonResult ToJsonResult(IEnumerable<Pie> found)
        {
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return JsonResult(pies);
        }
    }
}