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
    public class PieHub : Hub, IEventHandler<SliceAddedEvent>, IEventHandler<SlicePercentageRejectedEvent>
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

        public void Handle(IPublishedEvent<SliceAddedEvent> @event)
        {
            var model = new SliceModel { SliceId = @event.Payload.SliceId, Percent = @event.Payload.Percent, Description = @event.Payload.Description, PieId = @event.EventSourceId };
            var view = _controller.Render("_EditableSlice", model);
            _hubContext.Clients.All.sliceAdded(view);
        }

        public void Handle(IPublishedEvent<SlicePercentageRejectedEvent> @event)
        {
            _hubContext.Clients.All.slicePercentageRejected( new {sliceId = @event.Payload.SliceId, 
                                                                  currentPercent = @event.Payload.CurrentPercent,
                                                                  message = string.Format(Resources.RejectedPercentage, @event.Payload.RejectedPercent)} );
        }
    }
}