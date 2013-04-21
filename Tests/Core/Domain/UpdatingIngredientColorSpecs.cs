﻿using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.UpdatingIngredientColorSpecs
{
    [Concern(typeof (Pie))]
    public class When_updating_the_color_of_an_ingredient : PieSpecBase
    {
        private string _expectedColor;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
            _ingredientToUpdate = Ingredients[0];
            _expectedColor = "#ffffff";
        }

        protected override void When()
        {
            Sut.UpdateIngredientColor(_ingredientToUpdate.Id, _expectedColor);
        }

        [Observation]
        public void Then_should_announce_that_the_color_was_updated_for_the_ingredient()
        {
            Verify<IngredientColorUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewColor == _expectedColor).WasPublished();
        }
    }
}