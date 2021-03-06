﻿using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientDeletedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Ingredient Deleted { get; private set; }

        public IEnumerable<Ingredient> Ingredients { get; private set; }

        public Ingredient Filler { get; private set; }

        public IngredientDeletedEvent(Ingredient deleted, IEnumerable<Ingredient> allIngredients, Ingredient filler)
        {
            Deleted = deleted;
            Ingredients = allIngredients;
            Filler = filler;
        }
    }
}