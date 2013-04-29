using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class MaxIngredientsReachedEvent : SourcedEvent
    {
        public string Message { get; private set; }

        public MaxIngredientsReachedEvent(string message)
        {
            Message = message;
        }
    }
}