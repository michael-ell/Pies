using Codell.Pies.Common;
using EventStore;
using EventStore.Dispatcher;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Data.Storage
{
    public class EventDispatcher : IDispatchCommits
    {
        private readonly IEventBus _bus;

        public EventDispatcher(IEventBus bus)
        {
            Verify.NotNull(bus, "bus");            
            _bus = bus;
        }

        public void Dispatch(Commit commit)
        {
            _bus.Publish(commit.Events.ToCommittedEventStream(commit.StreamId));
        }

        public void Dispose()
        {
            //dispose anything???
        }
    }
}