using System;
using System.Collections.Generic;
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
                var list = new List<UncommittedEvent>(eventStream);
                for (var i = 0; i < list.Count; i++)
                {
                    var uncommitted = list[i];

                    //For some reason, some uncommitted events are duplicated within seconds.  We are assuming the event sequence
                    //should never equal the index, it should always be one greater to be a valid event.
                    if (uncommitted.EventSequence != i)
                    {
                        var message = new EventMessage {Body = uncommitted.Payload};
                        message.CommitId(eventStream.CommitId);
                        stream.Add(message);
                    }
                }
                try
                {
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