using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientAddedEvent : SourcedEvent
    {
        public string Description { get; private set; }

        public int Percent { get; private set; }

        public Guid Id { get; private set; }

        public IngredientAddedEvent(string description, int percent, Guid id)
        {
            Percent = percent;
            Description = description;
            Id = id;
        }
    }
}