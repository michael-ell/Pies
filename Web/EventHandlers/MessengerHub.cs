using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    [HubName("messenger")]
    public class MessengerHub : Hub, IEventHandler<PieStartedEvent>
    {
        private readonly IHubContext _hubContext;

        public MessengerHub(IHubContext hubContext)
        {
            Verify.NotNull(hubContext, "hubContext");
            _hubContext = hubContext;
        }

        public void Handle(IPublishedEvent<PieStartedEvent> evnt)
        {
            _hubContext.Clients.All.pieStarted(evnt.EventSourceId);
        }
    }
}