using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models;
using Pie = Codell.Pies.Core.ReadModels.Pie;

namespace Codell.Pies.Web.Controllers
{
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
        public JsonResult Find(string tag)
        {
            var found = _repository.Find<Pie>(pie => pie.Tags.Contains<string>(new SearchableTag(tag)));
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return JsonResult(pies);
        }
    }
}