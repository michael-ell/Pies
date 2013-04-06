using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class PercentageUpdatedEventCreator : Creator<PercentageUpdatedEvent>
    {
        public PercentageUpdatedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new PercentageUpdatedEvent(Guid.NewGuid(), 35, new List<Ingredient>{New.Domain().Ingredient()}, New.Domain().Ingredient());
        }

        public PercentageUpdatedEventCreator WithNoFiller()
        {
            Creation = new PercentageUpdatedEvent(Creation.Id, Creation.NewPercent, Creation.AllIngredients, New.Domain().Ingredient().Percent(0));
            return this;
        }
    }
}