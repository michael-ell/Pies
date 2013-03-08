using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieSliceAddedEvent : SourcedEvent
    {
        public int Percent { get; private set; }

        public string Description { get; private set; }

        public Guid SliceId { get; private set; }

        public PieSliceAddedEvent(int percent, string description, Guid sliceId)
        {
            Percent = percent;
            Description = description;
            SliceId = sliceId;
        }
    }
}