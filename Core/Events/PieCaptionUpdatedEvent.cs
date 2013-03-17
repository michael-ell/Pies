using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieCaptionUpdatedEvent : SourcedEvent
    {
        public string Caption { get; private set; }

        public PieCaptionUpdatedEvent(string caption)
        {
            Caption = caption;
        }
    }
}