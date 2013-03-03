using System;
using System.Collections.Generic;
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
        public ActionResult New()
        {
            var id = Guid.NewGuid();            
            _commandService.Execute(new StartPieCommand(id));
            var model = new PieModel
                            {
                                Id = id,
                                Slices = new List<SliceModel>{ new SliceModel {PieId = id} }
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Slice(SliceModel model)
        {
            _commandService.Execute(new SlicePieCommand(model.PieId, model.Percent, model.Description));
            return new EmptyResult();
        }
    }
}
