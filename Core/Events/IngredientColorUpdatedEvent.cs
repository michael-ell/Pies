using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientColorUpdatedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Guid Id { get; private set; }
        public string NewColor { get; private set; }
        public IEnumerable<Ingredient> Ingredients { get; private set; }
        public Ingredient Filler { get; private set; }

        public IngredientColorUpdatedEvent(Guid id, string color, IEnumerable<Ingredient> allIngredients, Ingredient filler)
        {
            Id = id;
            NewColor = color;
            Ingredients = allIngredients;
            Filler = filler;
        }
    }
}