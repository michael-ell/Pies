using Codell.Pies.Core.Domain;
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
            _ingredientToUpdate = AddIngredient("blueberries");
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

    [Concern(typeof(Pie))]
    public class When_updating_the_color_of_an_ingredient_that_has_not_changed : PieSpecBase
    {
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("cinammon");
        }

        protected override void When()
        {
            Sut.UpdateIngredientColor(_ingredientToUpdate.Id, _ingredientToUpdate.Color);
        }

        [Observation]
        public void Then_should_not_announce_that_the_color_was_updated_for_the_ingredient()
        {
            Verify<IngredientColorUpdatedEvent>().WasNotPublished();
        }
    }
}