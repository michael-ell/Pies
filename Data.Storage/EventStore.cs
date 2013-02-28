using System;
using System.Linq;
using EventStore;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;

namespace Codell.Pies.Data.Storage
{
    public class EventStore : IEventStore
    {
        private readonly IStoreEvents _store;
        private readonly bool _markCommitsAsDispatchedOnException;

        public static class MarkCommitsAsDispatchedOnException
        {
            public static bool Yes = true;
            public static bool No;
        }

        public EventStore(IStoreEvents store) : this(store, MarkCommitsAsDispatchedOnException.No)
        {
            if (store == null) throw new ArgumentNullException("store");
            _store = store;
        }

        public EventStore(IStoreEvents store, bool markCommitsAsDispatchedOnException)
        {
            if (store == null) throw new ArgumentNullException("store");
            _store = store;
            _markCommitsAsDispatchedOnException = markCommitsAsDispatchedOnException;
        }

        public CommittedEventStream ReadFrom(Guid id, long minVersion, long maxVersion)
        {
            using (var stream = _store.OpenStream(id, (int)minVersion, (int)maxVersion))
            { 
                return stream.CommittedEvents.ToCommittedEventStream(id);
            }
        }

        public void Store(UncommittedEventStream eventStream)
        {
            if (!eventStream.Any()) return;

            using (var stream = _store.OpenStream(eventStream.SourceId, 0, int.MaxValue))
            {
                foreach (var eventMessage in eventStream.Select(uncommittedEvent => new EventMessage { Body = uncommittedEvent.Payload }))
                {
                    eventMessage.CommitId(eventStream.CommitId);
                    stream.Add(eventMessage);
                }
                try
                {
                    //stream.CommitChanges(Guid.NewGuid());
                    stream.CommitChanges(eventStream.CommitId);
                }
                catch (Exception)
                {
                    if (_markCommitsAsDispatchedOnException)
                    {
                        _store.Advanced.MarkCommitAsDispatched(stream.ToCommit(eventStream.CommitId));
                    }
                    throw;
                }

            }
        }
    }
}