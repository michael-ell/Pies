using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IngredientColorUpdatedEventCreator : Creator<IngredientColorUpdatedEvent>
    {
        public IngredientColorUpdatedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new IngredientColorUpdatedEvent(Guid.NewGuid(), "#ffffff", new List<Ingredient> { New.Domain().Ingredient() }, New.Domain().Ingredient());
        }
    }
}