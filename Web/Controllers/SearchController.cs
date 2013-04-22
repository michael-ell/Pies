using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;

namespace Codell.Pies.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRepository _repository;

        public SearchController(IRepository repository)
        {
            Verify.NotNull(repository, "repository");
            
            _repository = repository;
        }

        public ActionResult GetTags(string criteria)
        {
            var found = _repository.Find<Tag>(tag => tag.Value.Contains(new Core.Domain.Tag(criteria)));
            return new EmptyResult();
        }
    }
}