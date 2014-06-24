using System;
using System.Collections.Generic;
using System.Web.Http;
using Codell.Pies.Common;
using Codell.Pies.Core.Commands;
using Codell.Pies.Web.Models.Providers;
using Codell.Pies.Web.Models.Shared;
using Codell.Pies.Web.Security;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Web.Areas.Api.Controllers
{
    [Authorize]
    public class MyPiesController : ApiController
    {
        private readonly IPiesProvider _piesProvider;
        private readonly ICommandService _commandService;

        public MyPiesController(IPiesProvider piesProvider, ICommandService commandService)
        {
            Verify.NotNull(piesProvider, "piesProvider");           
            Verify.NotNull(commandService, "commandService");
            
            _piesProvider = piesProvider;
            _commandService = commandService;
        }

        [HttpGet]
        public IEnumerable<PieModel> GetAll(IPiesIdentity identity)
        {
            return _piesProvider.GetPiesFor(identity);
        }

        [HttpDelete]
        public void Delete(Guid id)
        {
            _commandService.Execute(new DeletePieCommand(id));
        }         
    }
}