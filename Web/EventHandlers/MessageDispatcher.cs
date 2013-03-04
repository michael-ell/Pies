using Codell.Pies.Core.Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    public class MessageDispatcher : IEventHandler<PieSlicedEvent>
    {
        public void Handle(IPublishedEvent<PieSlicedEvent> evnt)
        {
        }
    }
}