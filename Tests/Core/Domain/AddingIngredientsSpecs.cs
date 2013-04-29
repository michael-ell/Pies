using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Common;

namespace Codell.Pies.Tests.Core.Domain.AddingIngredientsSpecs
{
    [Concern(typeof(Pie))]
    public class When_an_ingredient_is_added: PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            _description = "cinnamon";
            MockFor<ISettings>().Setup(settings => settings.Get<int>(Keys.MaxIngredients)).Returns(100);
        }

        protected override void When()
        {
            Sut.AddIngredient(_description, GetDependency<ISettings>());
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
        public void Then_should_assign_a_color_to_the_ingredient()
        {
            Verify<IngredientAddedEvent>(e => e.Added.Color.IsNotEmpty()).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.Ingredients.Contains()).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_an_ingredient_is_added_but_it_already_exists : PieSpecBase
    {
        private string _description;

        protected override void Given()
        {
            _description = "cinnamon";
            MockFor<ISettings>().Setup(settings => settings.Get<int>(Keys.MaxIngredients)).Returns(100);
            Sut.AddIngredient(_description, GetDependency<ISettings>());
        }

        protected override void When()
        {
            Sut.AddIngredient(_description, GetDependency<ISettings>());
        }

        [Observation]
        public void Then_should_not_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>().WasNotPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_trying_add_an_ingredient_but_the_max_ingredients_has_been_reached : PieSpecBase
    {
        private int _max;

        protected override void Given()
        {
            _max = 1;
            MockFor<ISettings>().Setup(settings => settings.Get<int>(Keys.MaxIngredients)).Returns(_max);
            Sut.AddIngredient("cinnamon", GetDependency<ISettings>());
        }

        protected override void When()
        {
            Sut.AddIngredient("blueberries", GetDependency<ISettings>());
        }

        [Observation]
        public void Then_should_announce_that_the_max_ingredients_have_been_reached()
        {
            Verify<MaxIngredientsReachedEvent>(e => e.Message == string.Format(Resources.MaxIngredientsReached, _max)).WasPublished();
        }

        [Observation]
        public void Then_should_not_announce_that_the_ingredient_was_added()
        {
            Verify<IngredientAddedEvent>().WasNotPublished();
        }
    }

}