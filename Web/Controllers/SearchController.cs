using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models;
using Codell.Pies.Web.Security;
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

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetTags(string term)
        {
            var tags = _repository.Find<Tag>(tag => tag.Value.Contains(new SearchableTag(term))).Select(tag => tag.Value).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult Find(string tag)
        {
            return Find(pie => pie.Tags.Contains<string>(new SearchableTag(tag)));    
        }

        [HttpGet]
        public JsonResult FindMyPies(IPiesIdentity identity)
        {
            return Find(pie => pie.UserEmail == identity.User.Email);   
        }

        private JsonResult Find(Expression<Func<Pie, bool>> predicate)
        {
            var found = _repository.Find(predicate);
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return JsonResult(pies);               
        }
    }
}