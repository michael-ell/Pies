using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class ProposedPercentageChangedEventCreator : Creator<ProposedPercentageChangedEvent>
    {
        public ProposedPercentageChangedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new ProposedPercentageChangedEvent(Guid.NewGuid(), 20, 15,
                                                          new List<Ingredient> {New.Domain().Ingredient()},
                                                          New.Domain().Ingredient());
        }
    }
}