using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieCreatedEvent : SourcedEvent
    {
        public string Name { get; private set; }

        public PieCreatedEvent(string name)
        {
            Name = name;
        }
    }
}