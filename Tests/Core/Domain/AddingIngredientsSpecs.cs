using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.AddingIngredientsSpecs
{
    [Concern(typeof(Pie))]
    public class When_an_ingredient_is_added: PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            _description = "cinnamon";
        }

        protected override void When()
        {
            Sut.AddIngredient(_description);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>(e => e.Added.Description == _description).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_is_0_percent()
        {
            Verify<IngredientAddedEvent>(e => e.Added.Percent == 0).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_filler_ingredient()
        {
            Verify<IngredientAddedEvent>(e => e.Filler.Percent == Pie.Max).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.AllIngredients.Contains()).WasPublished();
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