using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
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
        private readonly ISettings _settings;

        public HomeController(IRepository repository, IMappingEngine mapper, ISettings settings)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
                        
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pageSize = _settings.Get<int>(Keys.PiesPerPage);
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
            var totalPages = (_repository.Get<Pie>().Count(pie => pie.IsEmpty == false) + (pageSize - 1)) / pageSize;
            return View(new IndexModel
                            {
                                Pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(pies),
                                Paging = new PagingModel{CurrentPage = page.Value, TotalPages = totalPages}
                            });
        }

        [HttpGet]
        public JsonResult GetTags(string term)
        {
            var tags = _repository.Find<Tag>(tag => tag.Value.Contains(new SearchableTag(term))).Select(tag => tag.Value).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult Share(Guid id)
        {
            var pie = _repository.FindById<Guid, Pie>(id) ?? new Pie();
            return View("Index", new IndexModel
                                        {
                                            Pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(new[] { pie }),
                                        });
        }

        private JsonResult ToJsonResult(IEnumerable<Pie> found)
        {
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return JsonResult(pies);
        }
    }
}