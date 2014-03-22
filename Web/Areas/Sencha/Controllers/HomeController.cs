using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Infrastructure;
using Codell.Pies.Web.Models.Home;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;
        private readonly ISettings _settings;

        public HomeController(IRepository repository, IMappingEngine mapper, ISettings settings)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
            Verify.NotNull(settings, "settings");
                        
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            return Redirect(string.Format("{0}{1}", Url.Content(_settings.Get<string>(Keys.MobilePage)), id.IsEmpty() ? "" : "?share=" + id));
        }

        [HttpGet]
        public JsonNetResult GetRecent()
        {
            return ToJsonResult(_repository.Find<Pie>(pie => pie.IsEmpty == false).OrderByDescending(pie => pie.CreatedOn).Take(12));
        }

        [HttpGet]
        public JsonNetResult Get(string id)
        {
            Guid guid;
            return Guid.TryParse(id, out guid) ? ToJsonResult(new[] {_repository.FindById<Guid, Pie>(guid)}) : GetRecent();
        }

        [HttpGet]
        public ActionResult Share(Guid id)
        {
            return Index(id.ToString());
        }

        private JsonNetResult ToJsonResult(IEnumerable<Pie> found)
        {
            var pies = _mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(found);
            return new JsonNetResult {Data = pies};
        }
    }
}
