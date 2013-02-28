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

        public static CommittedEventStream ToCommittedEventStream(this IEnumerable<EventMessage> events,  Guid sourceId)
        {
            var committedEventStream 
                = new CommittedEventStream(sourceId, events == null ? new List<CommittedEvent>() : events.Select(ToCommittedEvent));
            return committedEventStream;
        }

        public static CommittedEvent ToCommittedEvent(this EventMessage eventMessage)
        {
            var e = (SourcedEvent)eventMessage.Body;
            return new CommittedEvent((Guid)eventMessage.Headers[CommitIdKey], 
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