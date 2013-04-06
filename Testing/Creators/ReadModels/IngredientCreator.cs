using System;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.ReadModels
{
    public class IngredientCreator : Creator<Ingredient>
    {
        public IngredientCreator(IFixtureContext context) : base(context, new Ingredient())
        {
            Creation.Description = Guid.NewGuid().ToString();
            Creation.Percent = 15;
        }
    }
}