using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.Creators.Common;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class PieCreatedEventCreator : Creator<PieCreatedEvent>
    {
        public PieCreatedEventCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new PieCreatedEvent(New.Common().User(), Guid.NewGuid().ToString(), new List<Ingredient>(), New.Domain().Ingredient());
        }
    }
}