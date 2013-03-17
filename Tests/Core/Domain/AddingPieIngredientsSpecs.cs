using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.AddingPieIngredientsSpecs
{
    [Concern(typeof(Pie))]
    public class When_adding_the_first_ingredient : PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            _description = "blueberries";
        }

        protected override void When()
        {
            Sut.AddIngredient(_description);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>(e => e.Description == _description).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_is_100_percent()
        {
            Verify<IngredientAddedEvent>(e => e.Percent == 100).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_an_ingredient_is_added_that_is_not_the_first_ingredient : PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            Sut.AddIngredient("blueberries");
            _description = "cinnamon";
        }

        protected override void When()
        {
            Sut.AddIngredient(_description);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>(e => e.Description == _description).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_is_0_percent()
        {
            Verify<IngredientAddedEvent>(e => e.Percent == 0).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_an_ingredient_is_added_but_it_already_exists : PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            _description = "cinnamon";
            Sut.AddIngredient(_description);
        }

        protected override void When()
        {
            Sut.AddIngredient(_description);
        }

        [Observation]
        public void Then_should_not_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>().WasNotPublished();
        }
    }

}