using System;
using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
using Codell.Pies.Web.Models;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Web.Controllers
{
    public class PieController : Controller
    {
        private readonly ICommandService _commandService;

        public PieController(ICommandService commandService)
        {
            Verify.NotNull(commandService, "commandService");
            
            _commandService = commandService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreatePieModel{Id = Guid.NewGuid()});
        }

        [HttpPost]
        public void Create(CreatePieModel model)
        {
            _commandService.Execute(new CreatePieCommand(model.Id, model.Name));            
        }

        [HttpPost]
        public void UpdateSlicePercentage(UpdateSlicePercentageModel model)
        {
            _commandService.Execute(new UpdateSlicePercentageCommand(model.SliceId, model.Percent, model.PieId));
        }
    }
}
