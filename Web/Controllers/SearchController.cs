using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;
using Pie = Codell.Pies.Core.ReadModels.Pie;

namespace Codell.Pies.Web.Controllers
{
    [AllowAnonymous]
    public class SearchController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public SearchController(IRepository repository, IMappingEngine mapper)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
            
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public JsonResult GetTags(string term)
        {
            var tags = _repository.Find<Tag>(tag => tag.Value.Contains(new SearchableTag(term))).Select(tag => tag.Value).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return ToJsonResult(_repository.GetAll<Pie>());
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
        public ActionResult Single(Guid id)
        {
            var pie = _repository.FindById<Guid, Pie>(id);
            return View(pie == null ? new PieModel() : _mapper.Map<Pie, PieModel>(pie));
        }

        private JsonResult ToJsonResult(IEnumerable<Pie> found)
        {
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return JsonResult(pies);             
        }
    }
}