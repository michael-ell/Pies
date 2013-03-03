using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Validation.ValidationResultSpecs
{
    [Concern(typeof(ValidationResult<ItemToValidate>))]
    public class When_valid_rules_have_been_added : ContextBase<ValidationResult<ItemToValidate>>
    {
        private ItemToValidate _validEntity;
        private ValidRule<ItemToValidate> _validRule;

        protected override void Given()
        {
            _validEntity = new ItemToValidate();
            _validRule = new ValidRule<ItemToValidate>(_validEntity);
        }

        protected override void When()
        {
            Sut.Add(_validRule);
        }

        [Observation]
        public void Then_the_associated_entity_should_be_valid()
        {
            Sut.ValidEntities.Should().Contain(_validEntity);
        }

        [Observation]
        public void Then_the_valid_rule_should_accessible()
        {
            Sut.ValidatedRules.Should().Contain(_validRule);
        }

        [Observation]
        public void Then_should_not_have_broken_rules()
        {
            Sut.HasBrokenRules.Should().BeFalse();
        }
    }

    [Concern(typeof(ValidationResult<object>))]
    public class When_invalid_rules_have_been_added : ContextBase<ValidationResult<ItemToValidate>>
    {
        private ItemToValidate _invalidEntity;
        private InvalidRule<ItemToValidate> _invalidRule;

        protected override void Given()
        {
            _invalidEntity = new ItemToValidate();
            _invalidRule = new InvalidRule<ItemToValidate>(_invalidEntity, "some error message");
        }

        protected override void When()
        {
            Sut.Add(_invalidRule);
        }

        [Observation]
        public void Then_the_associated_entity_should_be_invalid()
        {
            Sut.InvalidEntities.Should().Contain(_invalidEntity);
        }

        [Observation]
        public void Then_the_invalid_rule_message_should_accessible()
        {
            Sut.DistinctBrokenRules.Should().Contain(_invalidRule.ErrorMessage);
        }

        [Observation]
        public void Then_should_have_broken_rules_grouped_by_the_entity()
        {
            Sut.BrokenRules.Should().Contain(contextual => contextual.Context == _invalidEntity && contextual.Messages.Contains(_invalidRule.ErrorMessage));
        }

        [Observation]
        public void Then_should_have_broken_rules()
        {
            Sut.HasBrokenRules.Should().BeTrue();
        }
    }

    [Concern(typeof (ValidationResult<>))]
    public class When_invalid_group_rules_are_added : ContextBase<ValidationResult<object>>
    {
        private string _expectedErrorMessage;
        private IEnumerable<object> _invalidGroup;

        protected override void Given()
        {
            _expectedErrorMessage = "xx";
            _invalidGroup = new List<object>();
        }

        protected override void When()
        {
            Sut.Add(new InvalidRule<IEnumerable<object>>(_invalidGroup, _expectedErrorMessage));
        }

        [Observation]
        public void Then_should_have_broken_rules()
        {
            Sut.HasBrokenRules.Should().BeTrue();
        }

        [Observation]
        public void Then_should_indicate_also_has_broken_group_rules()
        {
            Sut.HasBrokenGroupRules.Should().BeTrue();
        }

        [Observation]
        public void Then_should_have_broken_rules_grouped_by_the_entity_group()
        {
            Sut.BrokenGroupRules.Should().Contain(contextual => contextual.Context == _invalidGroup && contextual.Messages.Contains(_expectedErrorMessage));
        }
         
    }

    [Concern(typeof(ValidationResult<object>))]
    public class When_asking_for_broken_rules_and_the_invalid_rules_have_been_added_that_have_the_same_error_message : ContextBase<ValidationResult<ItemToValidate>>
    {
        private IEnumerable<string> _brokenRules;
        private string _sameErrorMessage;

        protected override void Given()
        {
            _sameErrorMessage = "some error message";
            var invalidRule1 = new InvalidRule<ItemToValidate>(new ItemToValidate(), _sameErrorMessage);
            var invalidRule2 = new InvalidRule<ItemToValidate>(new ItemToValidate(), _sameErrorMessage);
            Sut.Add(invalidRule1);
            Sut.Add(invalidRule2);
        }

        protected override void When()
        {
            _brokenRules = Sut.DistinctBrokenRules;
        }

        [Observation]
        public void Then_should_only_return_distinct_error_messages()
        {
            _brokenRules.Count().Should().Be(1);
            _brokenRules.First().Should().Be(_sameErrorMessage);
        }
    }

    [Concern(typeof(ValidationResult<object>))]
    public class When_a_validated_entity_has_both_a_valid_rule_and_an_invalid_rule : ContextBase<ValidationResult<ItemToValidate>>
    {
        private ItemToValidate _someEntity;
        private InvalidRule<ItemToValidate> _invalidRule;
        private ValidRule<ItemToValidate> _validRule;

        protected override void Given()
        {
            _someEntity = new ItemToValidate();
            _invalidRule = new InvalidRule<ItemToValidate>(_someEntity, "some error message");
            _validRule = new ValidRule<ItemToValidate>(_someEntity);
            Sut.Add(_invalidRule);
            Sut.Add(_validRule);
        }

        protected override void When()
        {            
        }

        [Observation]
        public void Then_the_entity_should_be_invalid()
        {
            Sut.InvalidEntities.Should().Contain(_someEntity);
        }

        [Observation]
        public void Then_the_entity_should_not_be_valid()
        {
            Sut.ValidEntities.Should().NotContain(_someEntity);
        }

        [Observation]
        public void Then_should_have_broken_rules()
        {
            Sut.HasBrokenRules.Should().BeTrue();
        }
    }

    [Concern(typeof (ValidationResult<object>))]
    public class When_adding_a_validated_rule_that_already_exists : ContextBase<ValidationResult<ItemToValidate>>
    {
        private IValidatedRule<ItemToValidate> _validatedRule;

        protected override void Given()
        {
            _validatedRule = GetMock<IValidatedRule<ItemToValidate>>().Object;
            Sut.Add(_validatedRule);
        }

        protected override void When()
        {
            Sut.Add(_validatedRule);
        }

        [Observation]
        public void Then_should_not_add_it_twice()
        {
            Sut.ValidatedRules.Count().Should().Be(1);
        }
    }

    [Concern(typeof (ValidationResult<object>))]
    public class When_adding_validation_results_that_have_valid_and_invalid_rules_to_another_validation_result : ContextBase<ValidationResult<ItemToValidate>>
    {
        private ValidationResult<ItemToValidate> _validationResults;
        private IValidatedRule<ItemToValidate> _validRule;
        private IValidatedRule<ItemToValidate> _invalidRule;
        private ItemToValidate _validEntity;
        private ItemToValidate _invalidEntity;

        protected override void Given()
        {
            _validationResults = new ValidationResult<ItemToValidate>();
            _validEntity = new ItemToValidate();
            _validRule = new ValidRule<ItemToValidate>(_validEntity);
            _invalidEntity = new ItemToValidate();
            _invalidRule = new InvalidRule<ItemToValidate>(_invalidEntity, "some error message");
            _validationResults.Add(_validRule);
            _validationResults.Add(_invalidRule);
        }

        protected override void When()
        {
            Sut.Add(_validationResults);
        }

        [Observation]
        public void Then_invalid_entites_should_be_added()
        {
            Sut.InvalidEntities.Should().Contain(_invalidEntity);
        }

        [Observation]
        public void Then_valid_entities_should_be_added()
        {
            Sut.ValidEntities.Should().Contain(_validEntity);
        }

        [Observation]
        public void Then_the_validated_rules_should_be_added()
        {
            Sut.ValidatedRules.Should().Contain(_invalidRule);
            Sut.ValidatedRules.Should().Contain(_validRule);
        }

        [Observation]
        public void Then_should_have_broken_rules()
        {
            Sut.HasBrokenRules.Should().BeTrue();
        }
    }

    [Concern(typeof (ValidationResult<object>))]
    public class When_a_valid_entity_has_many_rules : ContextBase<ValidationResult<ItemToValidate>>
    {
        private ItemToValidate _validEntity;
        private ValidRule<ItemToValidate> _firstValidRule;
        private ValidRule<ItemToValidate> _secondValidRule;
        private IEnumerable<ItemToValidate> _validEntities;

        protected override void Given()
        {
            _validEntity = new ItemToValidate();
            _firstValidRule = new ValidRule<ItemToValidate>(_validEntity);
            _secondValidRule = new ValidRule<ItemToValidate>(_validEntity);
            Sut.Add(_firstValidRule);
            Sut.Add(_secondValidRule);
        }

        protected override void When()
        {
            _validEntities =  Sut.ValidEntities;
        }

        [Observation]
        public void Then_it_should_only_appear_once_as_valid_entity()
        {
            _validEntities.Count(entity => entity == _validEntity).Should().Be(1);
        }
    }
}