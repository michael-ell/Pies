using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;
using Pie = Codell.Pies.Core.ReadModels.Pie;

namespace Codell.Pies.Web.Areas.Api.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
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
        public IEnumerable<PieModel> GetRecent()
        {
            var pies = _repository.Find<Pie>(pie => pie.IsPrivate == false && pie.IsEmpty == false)
                                  .OrderByDescending(pie => pie.CreatedOn)
                                  .Take(12);
            return ToModels(pies);
        }

        [HttpGet]
        public IEnumerable<PieModel> Find(string tag = "")
        {
            return tag.IsEmpty() ? GetRecent() : ToModels(_repository.Find<Pie>(pie => pie.IsPrivate == false && pie.Tags.Contains<string>(new SearchableTag(tag))));
        }

        [HttpGet]
        public IEnumerable<string> GetTags(string term)
        {
            return _repository.Find<Tag>(tag => tag.Value.Contains(new SearchableTag(term))).Select(tag => tag.Value).ToList();
        }

        [HttpGet]
        public IEnumerable<PieModel> Get(string id)
        {
            Pie pie = null;
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                pie = _repository.FindById<Guid, Pie>(guid);
            }
            return ToModels(new[] {pie ?? new Pie()});
        }

        private IEnumerable<PieModel> ToModels(IEnumerable<Pie> pies)
        {
            return _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(pies);
        }
    }
}
