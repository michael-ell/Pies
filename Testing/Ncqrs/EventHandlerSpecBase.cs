using System;
using Codell.Pies.Testing.BDD;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Testing.Ncqrs
{
    public abstract class EventHandlerSpecBase<TSut> : ContextBase<TSut> where TSut : class
    {
        protected PublishedEventFactory PublishedEvent
        {
            get { return new PublishedEventFactory(); }
        }

        protected class PublishedEventFactory
        {
            public PublishedEvent<T> For<T>() where T : SourcedEvent, new()
            {
                return CreatePublishedEventFor(new T());
            }

            public PublishedEvent<T> For<T>(T @event) where T : SourcedEvent
            {
                return CreatePublishedEventFor(@event);
            }

            private PublishedEvent<T> CreatePublishedEventFor<T>(T @event) where T : SourcedEvent
            {
                return new PublishedEvent<T>(new CommittedEvent(Guid.NewGuid(), Guid.NewGuid(), @event.EventSourceId, 1, DateTime.Now, @event, new Version()));
            }
        }
    }
}