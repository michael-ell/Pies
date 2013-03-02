using System;
using System.Web.Mvc;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
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
            _commandService.Execute(new StartPieCommand(Guid.NewGuid()));
            return View();
        }
    }
}
