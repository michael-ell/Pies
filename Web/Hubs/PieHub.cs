using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Web.Controllers;
using Codell.Pies.Web.Extensions;
using Codell.Pies.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    [HubName("pie")]
    public class PieHub : Hub, IEventHandler<SliceAddedEvent>
    {
        private readonly IHubContext _hubContext;
        private readonly PieController _controller;

        public PieHub(IHubContext hubContext, PieController controller)
        {
            Verify.NotNull(hubContext, "hubContext");
            Verify.NotNull(controller, "controller");            
            _hubContext = hubContext;
            _controller = controller;
        }

        public void Handle(IPublishedEvent<SliceAddedEvent> evnt)
        {
            var model = new SliceModel { SliceId = evnt.Payload.SliceId, Percent = evnt.Payload.Percent, Description = evnt.Payload.Description, PieId = evnt.EventSourceId };
            var view = _controller.Render("_EditableSlice", model);
            _hubContext.Clients.All.sliceAdded(view);
        }
    }
}