using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieCaptionUpdatedEvent : SourcedEvent
    {
        public string NewCaption { get; private set; }

        public PieCaptionUpdatedEvent(string newCaption)
        {
            NewCaption = newCaption;
        }
    }
}