using System;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models.Shared;
using Codell.Pies.Web.Security;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Web.Controllers
{
    [AllowAnonymous]
    public class PieController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public PieController(ICommandService commandService, IRepository repository, IMappingEngine mapper)
        {
            Verify.NotNull(commandService, "commandService");
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
                        
            _commandService = commandService;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create(IPiesIdentity identity)
        {
            var id = Guid.NewGuid();
            _commandService.Execute(new CreatePieCommand(id, identity.User));
            var pie = _repository.FindById<Guid, Pie>(id);
            return View("Edit", _mapper.Map<Pie, PieModel>(pie));
        }

        [HttpGet]
        public ActionResult Join(Guid id)
        {
            var pie = _repository.FindById<Guid, Pie>(id);
            var model = _mapper.Map<Pie, PieModel>(pie);
            model.Joining = true;
            return View("Edit", model);
        }
    }
}
