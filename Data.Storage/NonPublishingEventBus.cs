using System.Collections.Generic;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Data.Storage
{
    public class NonPublishingEventBus : IEventBus
    {
        public void Publish(IPublishableEvent eventMessage)
        {
            //let event store dispatch messages only when event is successfully stored
        }

        public void Publish(IEnumerable<IPublishableEvent> eventMessages)
        {
            //let event store dispatch messages only when event is successfully stored
        }
    }
}