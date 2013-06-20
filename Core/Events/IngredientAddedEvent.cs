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

        public IEnumerable<Ingredient> Ingredients { get; private set; }

        public Ingredient Filler { get; private set; }

        public string Message { get; set; }

        public IngredientAddedEvent(Ingredient added, IEnumerable<Ingredient> allIngredients, Ingredient filler, string message = "")
        {
            Added = added;
            Ingredients = allIngredients;
            Filler = filler;
            Message = message;
        }
    }
}