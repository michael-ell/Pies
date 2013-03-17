using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.DeletingPieIngredientSpecs
{
    [Concern(typeof(Pie))]
    public class When_deleting_an_ingredient_that_exists : PieSpecBase
    {
        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
        }

        protected override void When()
        {
            Sut.DeleteIngredient(Ingredients[0].Id);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_was_deleted()
        {
            Verify<IngredientDeletedEvent>(e => e.Id == Ingredients[0].Id).WasPublished();
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