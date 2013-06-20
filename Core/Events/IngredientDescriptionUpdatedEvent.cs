using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientDescriptionUpdatedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Guid Id { get; private set; }
        public string NewDescription { get; private set; }
        public IEnumerable<Ingredient> Ingredients { get; private set; }
        public Ingredient Filler { get; private set; }
        public string Message { get; private set; }

        public IngredientDescriptionUpdatedEvent(Guid id, string description, IEnumerable<Ingredient> allIngredients, Ingredient filler, string message = "")
        {
            Id = id;
            NewDescription = description;
            Ingredients = allIngredients;
            Filler = filler;
            Message = message;
        }
    }
}