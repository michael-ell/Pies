using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieTagsUpdatedEvent : SourcedEvent
    {
        public IEnumerable<Tag> NewTags { get; private set; }

        public PieTagsUpdatedEvent(IEnumerable<Tag> newTags)
        {
            NewTags = newTags;
        }
    }
}