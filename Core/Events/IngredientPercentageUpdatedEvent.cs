using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientPercentageUpdatedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Guid Id { get; private set; }
        public int NewPercent { get; private set; }
        public IEnumerable<Ingredient> Ingredients { get; private set; }
        public Ingredient Filler { get; private set; }

        public IngredientPercentageUpdatedEvent(Guid id, int percent, IEnumerable<Ingredient> allIngredients, Ingredient filler)
        {
            Id = id;
            NewPercent = percent;
            Ingredients = allIngredients;
            Filler = filler;
        }
    }
}