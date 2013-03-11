using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class SliceDeletedEvent : SourcedEvent
    {
        public Guid SliceId { get; private set; }

        public SliceDeletedEvent(Guid sliceId)
        {
            SliceId = sliceId;
        }
    }
}