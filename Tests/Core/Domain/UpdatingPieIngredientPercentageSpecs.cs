using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.UpdatingPieIngredientPercentageSpecs
{
    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_and_the_percent_is_negative : PieSpecBase
    {
        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(Ingredients[0].Id, -10);
        }

        [Observation]
        public void Then_should_announce_slice_percent_was_updated_to_zero()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Percent == 0 && e.Id == Ingredients[0].Id).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_an_ingredient_but_the_percentage_has_not_changed : PieSpecBase
    {
        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(Ingredients[0].Id, Ingredients[0].Percent);
        }

        [Observation]
        public void Then_should_not_announce_slice_percent_was_updated()
        {
            Verify<IngredientPercentageUpdatedEvent>().WasNotPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_that_would_account_for_more_than_100_percent_of_the_pie : PieSpecBase
    {
        private int _proposed;

        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
            _proposed = 110;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(Ingredients[0].Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_rejected()
        {
            Verify<IngredientPercentageRejectedEvent>(e => e.Id == Ingredients[0].Id && e.RejectedPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_current_percent_of_the_slice_that_was_not_updated()
        {
            Verify<IngredientPercentageRejectedEvent>(e => e.CurrentPercent == Ingredients[0].Percent).WasPublished();
        }
    }
}