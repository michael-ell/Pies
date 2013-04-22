using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IngredientPercentageUpdatedEventCreator : Creator<IngredientPercentageUpdatedEvent>
    {
        public IngredientPercentageUpdatedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new IngredientPercentageUpdatedEvent(Guid.NewGuid(), 35, new List<Ingredient>{New.Domain().Ingredient()}, New.Domain().Ingredient());
        }

        public IngredientPercentageUpdatedEventCreator WithNoFiller()
        {
            Creation = new IngredientPercentageUpdatedEvent(Creation.Id, Creation.NewPercent, Creation.Ingredients, New.Domain().Ingredient().Percent(0));
            return this;
        }
    }
}