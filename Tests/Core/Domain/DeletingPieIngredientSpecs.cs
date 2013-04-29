using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.DeletingPieIngredientSpecs
{
    [Concern(typeof(Pie))]
    public class When_deleting_an_ingredient_that_exists : PieSpecBase
    {
        private Ingredient _toDelete;
        private int _expectedFiller;

        protected override void Given()
        {
            AddIngredient("blueberries");
            _toDelete = AddIngredient("cinnamon");

            Sut.UpdateIngredientPercentage(_toDelete.Id, 20);
            Sut.UpdateIngredientPercentage(Ingredients[0].Id, 40);
            _expectedFiller = 60;

        }

        protected override void When()
        {
            Sut.DeleteIngredient(_toDelete.Id);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_was_deleted()
        {
            Verify<IngredientDeletedEvent>(e => e.Deleted == _toDelete).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_adjusted_filler_ingredient()
        {
            Verify<IngredientDeletedEvent>(e => e.Filler.Percent == _expectedFiller).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_deleting_an_ingredient_that_does_not_exist_in_the_pie : PieSpecBase
    {
        protected override void When()
        {
            Sut.DeleteIngredient(Guid.NewGuid());
        }

        [Observation]
        public void Then_should_not_announce_that_a_slice_was_deleted()
        {
            Verify<IngredientDeletedEvent>().WasNotPublished();
        }
    }
}