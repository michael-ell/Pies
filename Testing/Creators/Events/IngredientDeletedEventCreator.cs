using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.Creators.Domain;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IngredientDeletedEventCreator : Creator<IngredientDeletedEvent>
    {
        public IngredientDeletedEventCreator(IFixtureContext context) : base(context, null)
        {
            var deleted = New.Domain().Ingredient();
            var all = new List<Ingredient> { deleted, New.Domain().Ingredient() };
            Creation = new IngredientDeletedEvent(deleted, all, New.Domain().Ingredient());
        }
    }
}