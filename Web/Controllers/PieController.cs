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

namespace Codell.Pies.Web.Controllers
{
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
        public ActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<Pie>, IEnumerable<PieModel>>(_repository.GetAll<Pie>()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreatePieModel{Id = Guid.NewGuid()});
        }

        [HttpPost]
        public void Create(CreatePieModel model)
        {
            _commandService.Execute(new CreatePieCommand(model.Id));            
        }

        [HttpPost]
        public void UpdatePieCaption(UpdatePieCaptionModel model)
        {
            _commandService.Execute(new UpdatePieCaptionCommand(model.Id, model.Caption));
        }

        [HttpPost]
        public void UpdateIngredientPercentage(UpdateIngredientPercentageModel model)
        {
            _commandService.Execute(new UpdateIngredientPercentageCommand(model.SliceId, model.Percent, model.PieId));
        }

        [HttpDelete]
        public void DeleteIngredient(Guid id)
        {
            
        }
    }
}
