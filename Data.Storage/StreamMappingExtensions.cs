using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Data.Storage
{
    public static class StreamMappingExtensions
    {
        private const string CommitIdKey = "commitId";

        public static void CommitId(this EventMessage message, Guid id)
        {
            message.Headers[CommitIdKey] = id;
        }

        public static CommittedEventStream ToCommittedEventStream(this IEnumerable<EventMessage> events, Guid sourceId)
        {
            var committedEventStream
                = new CommittedEventStream(sourceId, events == null ? new List<CommittedEvent>() : ToCommittedEvents(events));
            return committedEventStream;
        }

        private static IEnumerable<CommittedEvent> ToCommittedEvents(this IEnumerable<EventMessage> events)
        {
            var list = new List<EventMessage>(events.Distinct(new SequenceComparer()).OrderBy(e => ((SourcedEvent)e.Body).EventSequence));
            for (var i = 0; i < list.Count; i++)
            {
                var message = list[i];
                var e = (SourcedEvent)message.Body;

                //For some reason, some events are written twice within seconds.  We are assuming the event sequence
                //should never equal the index, it should always be one greater to be a valid event.
                if (e.EventSequence != i)
                {
                    yield return ToCommittedEvent((Guid)message.Headers[CommitIdKey], e);
                }
            }
        }

        private class SequenceComparer : IEqualityComparer<EventMessage>
        {
            public bool Equals(EventMessage x, EventMessage y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return true;
                return ((SourcedEvent)x.Body).EventSequence == ((SourcedEvent)y.Body).EventSequence;
            }

            public int GetHashCode(EventMessage obj)
            {
                return obj == null ? 0 : ((SourcedEvent)obj.Body).EventSequence.GetHashCode();
            }
        }

        private static CommittedEvent ToCommittedEvent(Guid commitId, SourcedEvent e)
        {
            return new CommittedEvent(commitId,
                                      e.EventIdentifier,
                                      e.EventSourceId,
                                      e.EventSequence,
                                      e.EventTimeStamp.ToLocalTime(),
                                      e,
                                      e.EventVersion);
        }

        public static Commit ToCommit(this IEventStream stream, Guid commitId)
        {
            return new Commit(stream.StreamId,
                              stream.StreamRevision,
                              commitId,
                              stream.CommitSequence == 0 ? 1 : stream.CommitSequence + 1,
                              DateTime.Now,
                              new Dictionary<string, object>(stream.UncommittedHeaders),
                              new List<EventMessage>(stream.UncommittedEvents));
        }
    }
}