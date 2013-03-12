using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class SlicePercentageRejectedEvent : SourcedEvent
    {
        public Guid SliceId { get; private set; }

        public int RejectedPercent { get; private set; }

        public int CurrentPercent { get; private set; }

        public SlicePercentageRejectedEvent(Guid sliceId, int rejectedPercent, int currentPercent)
        {
            SliceId = sliceId;
            RejectedPercent = rejectedPercent;
            CurrentPercent = currentPercent;
        }
    }
}