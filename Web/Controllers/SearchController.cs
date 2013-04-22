using System.Linq;
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

        [HttpGet]
        public ActionResult GetTags(string term)
        {
            var tags = _repository.Find<Tag>(tag => tag.Value.Contains(term)).Select(tag => tag.Value).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}