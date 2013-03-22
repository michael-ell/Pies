using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientAddedEvent : SourcedEvent
    {
        public Ingredient IngredientAdded { get; private set; }

        public IEnumerable<Ingredient> AllIngredients { get; private set; }

        public IngredientAddedEvent(Ingredient ingredientAdded, IEnumerable<Ingredient> allIngredients)
        {
            IngredientAdded = ingredientAdded;
            AllIngredients = allIngredients;
        }
    }
}