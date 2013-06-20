using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.Services;
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
            _ingredientToUpdate = AddIngredient("blueberries");
            _expectedDescription = "cinammon";
            MockFor<ICleaner>().Setup(cleaner => cleaner.Clean(_expectedDescription)).Returns(new Cleaner.Result(false, _expectedDescription));
        }

        protected override void When()
        {
            Sut.UpdateIngredientDescription(_ingredientToUpdate.Id, _expectedDescription, GetDependency<ICleaner>());
        }

        [Observation]
        public void Then_should_announce_that_the_description_was_updated_for_the_ingredient()
        {
            Verify<IngredientDescriptionUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewDescription == _expectedDescription).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_description_of_an_ingredient_that_contains_a_bad_word : PieSpecBase
    {
        private string _dirtyDescription;
        private string _expectedCleanDescription;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            _dirtyDescription = "xxx cinammon";
            _expectedCleanDescription = "cinammon";
            MockFor<ICleaner>().Setup(cleaner => cleaner.Clean(_dirtyDescription)).Returns(new Cleaner.Result(true, _expectedCleanDescription));
        }

        protected override void When()
        {
            Sut.UpdateIngredientDescription(_ingredientToUpdate.Id, _dirtyDescription, GetDependency<ICleaner>());
        }

        [Observation]
        public void Then_should_announce_that_the_clean_description_was_updated_for_the_ingredient()
        {
            Verify<IngredientDescriptionUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewDescription == _expectedCleanDescription).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_a_dirty_word_was_detected()
        {
            Verify<IngredientDescriptionUpdatedEvent>(e => e.Message == Resources.DirtyWordDetected).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_description_of_an_ingredient_that_has_not_changed : PieSpecBase
    {
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("cinammon");
        }

        protected override void When()
        {
            Sut.UpdateIngredientDescription(_ingredientToUpdate.Id, _ingredientToUpdate.Description, GetDependency<ICleaner>());
        }

        [Observation]
        public void Then_should_not_announce_that_the_description_was_updated_for_the_ingredient()
        {
            Verify<IngredientDescriptionUpdatedEvent>().WasNotPublished();
        }
    }
}