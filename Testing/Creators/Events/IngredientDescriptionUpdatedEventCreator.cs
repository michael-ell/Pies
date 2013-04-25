using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IngredientDescriptionUpdatedEventCreator : Creator<IngredientDescriptionUpdatedEvent>
    {
        public IngredientDescriptionUpdatedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new IngredientDescriptionUpdatedEvent(Guid.NewGuid(), Guid.NewGuid().ToString(), new List<Ingredient> { New.Domain().Ingredient() }, New.Domain().Ingredient());
        }
    }
}