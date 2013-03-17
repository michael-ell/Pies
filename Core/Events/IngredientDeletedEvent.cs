using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientDeletedEvent : SourcedEvent
    {
        public Guid Id { get; private set; }

        public IngredientDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}