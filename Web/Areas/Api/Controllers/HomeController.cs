using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Providers;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Areas.Api.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        private readonly IPiesProvider _piesProvider;
        private readonly IRepository _repository;

        public HomeController(IPiesProvider piesProvider, IRepository repository)
        {
            Verify.NotNull(piesProvider, "piesProvider");
            Verify.NotNull(repository, "repository");
            
            _piesProvider = piesProvider;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<PieModel> GetRecent()
        {
            return _piesProvider.GetRecent();
        }

        [HttpGet]
        public IEnumerable<PieModel> Find(string tag = "")
        {
            return _piesProvider.Find(tag);
        }

        [HttpGet]
        public IEnumerable<string> GetTags(string term)
        {
            return _repository.Find<Tag>(tag => tag.Value.Contains(new SearchableTag(term))).Select(tag => tag.Value).ToList();
        }

        [HttpGet]
        public PieModel Get(string id)
        {
            return _piesProvider.Get(id);
        }
    }
}
