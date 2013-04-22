using System;
using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieTagsUpdatedEvent : SourcedEvent
    {
        public IEnumerable<string> NewTags { get; private set; }

        public PieTagsUpdatedEvent(IEnumerable<string> newTags)
        {
            NewTags = newTags;
        }
    }
}