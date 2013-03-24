using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientAddedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Ingredient Added { get; private set; }

        public IEnumerable<Ingredient> AllIngredients { get; private set; }

        public Ingredient Filler { get; private set; }

        public IngredientAddedEvent(Ingredient added, IEnumerable<Ingredient> allIngredients, Ingredient filler)
        {
            Added = added;
            AllIngredients = allIngredients;
            Filler = filler;
        }
    }
}