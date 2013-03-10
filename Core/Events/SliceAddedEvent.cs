using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class SliceAddedEvent : SourcedEvent
    {
        public int Percent { get; private set; }

        public string Description { get; private set; }

        public Guid SliceId { get; private set; }

        public SliceAddedEvent(int percent, string description, Guid sliceId)
        {
            Percent = percent;
            Description = description;
            SliceId = sliceId;
        }
    }
}