using System;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Infrastructure;
using Codell.Pies.Web.Models.Pie;
using Codell.Pies.Web.Models.Shared;
using Codell.Pies.Web.Security;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Web.Areas.Sencha.Controllers
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
        public JsonNetResult Create(IPiesIdentity identity)
        {
            var id = Guid.NewGuid();
            _commandService.Execute(new CreatePieCommand(id, identity.User));
            var pie = _repository.FindById<Guid, Pie>(id);
            return new JsonNetResult { Data = _mapper.Map<Pie, PieModel>(pie) };
        }

        [HttpPost]
        public void UpdateCaption(UpdateCaptionModel model)
        {
            _commandService.Execute(new UpdatePieCaptionCommand(model.Id, model.Caption));
        }

        [HttpPost]
        public void UpdateTags(UpdateTagsModel model)
        {
            _commandService.Execute(new UpdatePieTagsCommand(model.Id, model.Tags));
        }
    }
}