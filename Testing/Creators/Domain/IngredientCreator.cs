using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class IngredientCreator : Creator<Ingredient>
    {
        public IngredientCreator(IFixtureContext context)
            : base(context, new Ingredient(Guid.NewGuid(), Guid.NewGuid().ToString(), 20, Guid.NewGuid().ToString()))
        {
        }

        public IngredientCreator Percent(int percent)
        {
            Creation.Percent = percent;
            return this;
        }
    }
}