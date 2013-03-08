using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class SlicePercentageUpdatedEvent : SourcedEvent
    {
        public Guid SliceId { get; private set; }
        public int Percent { get; private set; }

        public SlicePercentageUpdatedEvent(Guid sliceId, int percent)
        {
            SliceId = sliceId;
            Percent = percent;
        }
    }
}