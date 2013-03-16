using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientPercentageUpdatedEvent : SourcedEvent
    {
        public Guid Id { get; private set; }
        public int Percent { get; private set; }

        public IngredientPercentageUpdatedEvent(Guid id, int percent)
        {
            Id = id;
            Percent = percent;
        }
    }
}