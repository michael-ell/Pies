using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieCreatedEvent : SourcedEvent
    {
    }
}