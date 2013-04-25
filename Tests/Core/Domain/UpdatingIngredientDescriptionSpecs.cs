using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.UpdatingIngredientDescriptionSpecs
{
    [Concern(typeof (Pie))]
    public class When_updating_the_description_of_an_ingredient : PieSpecBase
    {
        private string _expectedDescription;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
            _ingredientToUpdate = Ingredients[0];
            _expectedDescription = "cinammon";
        }

        protected override void When()
        {
            Sut.UpdateIngredientDescription(_ingredientToUpdate.Id, _expectedDescription);
        }

        [Observation]
        public void Then_should_announce_that_the_description_was_updated_for_the_ingredient()
        {
            Verify<IngredientDescriptionUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewDescription == _expectedDescription).WasPublished();
        }
    }
}