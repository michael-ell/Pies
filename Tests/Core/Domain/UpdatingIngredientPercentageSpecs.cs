using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Helpers;

namespace Codell.Pies.Tests.Core.Domain.UpdatingIngredientPercentageSpecs
{
    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_and_the_percent_is_negative : PieSpecBase
    {
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, 30);
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, -10);
        }

        [Observation]
        public void Then_should_announce_slice_percent_was_updated_to_zero()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.NewPercent == 0 && e.Id == _ingredientToUpdate.Id).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_an_ingredient_but_the_percentage_has_not_changed : PieSpecBase
    {
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, _ingredientToUpdate.Percent);
        }

        [Observation]
        public void Then_should_not_announce_slice_percent_was_updated()
        {
            Verify<IngredientPercentageUpdatedEvent>().WasNotPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_increasing_the_percentage_of_a_ingredient_for_a_pie_that_has_no_filler : PieSpecBase
    {
        private int _proposed;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, 100);
            _proposed = 110;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_rejected()
        {
            Verify<PercentageRejectedEvent>(e => e.Id == _ingredientToUpdate.Id && e.RejectedPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_current_percent_of_the_slice_that_was_not_updated()
        {
            Verify<PercentageRejectedEvent>(e => e.CurrentPercent == _ingredientToUpdate.Percent).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_for_a_pie_that_is_less_than_the_filler : PieSpecBase
    {
        private int _proposed;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            _proposed = 10;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_updated()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_adjusted_filler_ingredient()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Filler.Percent == Pie.Max - _proposed).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            Verify<IngredientAddedEvent>(e => e.Ingredients.Matches(new List<Ingredient>{_ingredientToUpdate})).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_that_is_greater_than_the_filler : PieSpecBase
    {
        private int _proposed;
        private int _expected;
        private Guid _toChange;

        protected override void Given()
        {
            var set = AddIngredient("blueberries");
            _toChange = AddIngredient("cinammon").Id;
            Sut.UpdateIngredientPercentage(set.Id, 80);
            Sut.UpdateIngredientPercentage(_toChange, 15);
            _proposed = 30;
            _expected = 20;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_toChange, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_proposed_ingredient_percent_was_changed_to_the_remaining_filler()
        {
            Verify<ProposedIngredientPercentageChangedEvent>(e => e.Id == _toChange && e.AcceptedPercent == _expected).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_proposed_ingredient_percentage()
        {
            Verify<ProposedIngredientPercentageChangedEvent>(e => e.ProposedPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_there_is_no_more_filler()
        {
            Verify<ProposedIngredientPercentageChangedEvent>(e => e.Filler.Percent == 0).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.EditableIngredients.Contains()).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_increasing_the_percentage_of_a_ingredient_that_is_currently_the_same_as_the_filler : PieSpecBase
    {
        private int _proposed;
        private int _expectedPercent;
        private Guid _toChange;
        private int _expectedFiller;

        protected override void Given()
        {
            var set = AddIngredient("blueberries");
            _toChange = AddIngredient("cinammon").Id;
            Sut.UpdateIngredientPercentage(set.Id, 60);
            Sut.UpdateIngredientPercentage(_toChange, 20);
            _proposed = 25;
            _expectedPercent = 25;
            _expectedFiller = 15;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_toChange, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_updated()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Id == _toChange && e.NewPercent == _expectedPercent).WasPublished();
        }

        [Observation]
        public void Then_should_reduce_the_filler()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Filler.Percent == _expectedFiller).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.EditableIngredients.Contains()).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_ingredient_that_is_equal_to_the_filler : PieSpecBase
    {
        private int _proposed;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            _proposed = Pie.Max;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_updated_using_the_remaining_filler()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewPercent == Pie.Max).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_there_is_no_more_filler()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Filler.Percent == 0).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.EditableIngredients.Contains()).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_reducing_the_percentage_of_a_ingredient_and_there_is_no_filler: PieSpecBase
    {
        private int _proposed;
        private int _expectedFiller;
        private Ingredient _ingredientToUpdate;

        protected override void Given()
        {
            _ingredientToUpdate = AddIngredient("blueberries");
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, Pie.Max);
            _proposed = 20;
            _expectedFiller = 80;
        }

        protected override void When()
        {
            Sut.UpdateIngredientPercentage(_ingredientToUpdate.Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_ingredient_percent_was_updated()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Id == _ingredientToUpdate.Id && e.NewPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_there_now_is_a_filler()
        {
            Verify<IngredientPercentageUpdatedEvent>(e => e.Filler.Percent == _expectedFiller).WasPublished();
        }

        [Observation(Skip = "tbd")]
        public void Then_should_announce_all_ingredients()
        {
            //Verify<IngredientAddedEvent>(e => e.EditableIngredients.Contains()).WasPublished();
        }
    }
}