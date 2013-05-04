using System;
using System.Collections.Generic;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.ReadModels
{
    public class PieCreator : Creator<Pie>
    {
        public PieCreator(IFixtureContext context) : base(context, new Pie())
        {
            Creation.Caption = Guid.NewGuid().ToString();
            Creation.EditableIngredients = new List<Ingredient>{New.ReadModels().Ingredient()};
            Creation.CreatedOn = DateTime.Now;
        }
    }
}