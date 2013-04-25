using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Web.Models;
using Ncqrs.Commanding.ServiceModel;
using System.Linq;

namespace Codell.Pies.Web.Controllers
{
    public class PieController : ControllerBase
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
        public ActionResult Index()
        {
            var pies = _repository.Find<Pie>(pie => pie.IsEmpty == false).OrderByDescending(pie => pie.CreatedOn).Take(12);
            return View(_mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(pies));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var id = Guid.NewGuid();
            _commandService.Execute(new CreatePieCommand(id));
            var pie = _repository.FindById<Guid, Pie>(id);
            return View(_mapper.Map<Pie, PieModel>(pie));
        }

        [HttpPost]
        public void UpdateCaption(UpdateCaptionModel model)
        {
            _commandService.Execute(new UpdatePieCaptionCommand(model.Id, model.Caption));
        }

        [HttpPost]
        public void AddIngredient(AddIngredientModel model)
        {
            _commandService.Execute(new AddIngredientCommand(model.Id, model.Description));
        }

        [HttpPost]
        public void UpdateIngredientPercentage(UpdateIngredientPercentageModel model)
        {
            _commandService.Execute(new UpdateIngredientPercentageCommand(model.Id, model.Percent, model.PieId));
        }

        [HttpPost]
        public void UpdateIngredientColor(UpdateIngredientColorModel model)
        {
            _commandService.Execute(new UpdateIngredientColorCommand(model.Id, model.Color, model.PieId));
        }

        [HttpDelete]
        public void DeleteIngredient(DeleteIngredientModel model)
        {
            _commandService.Execute(new DeleteIngredientCommand(model.Id, model.PieId));  
        }

        [HttpPost]
        public void UpdateTags(UpdateTagsModel model)
        {
            _commandService.Execute(new UpdatePieTagsCommand(model.Id, model.Tags));
        }
    }
}
