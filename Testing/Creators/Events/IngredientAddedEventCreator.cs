using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;
using Codell.Pies.Testing.Creators.Domain;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IngredientAddedEventCreator : Creator<IngredientAddedEvent>
    {
       

        public IngredientAddedEventCreator(IFixtureContext context) : base(context, null)
        {
            var added = New.Domain().Ingredient();
            var all = new List<Ingredient> {added, New.Domain().Ingredient()};
            Creation = new IngredientAddedEvent(added, all, New.Domain().Ingredient());
        }
    }
}