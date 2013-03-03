using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;
using Codell.Pies.Tests.Core.Validation;

namespace Codell.Pies.Tests.Core.Validation.ConditionalRuleSpecs
{
    [Concern(typeof (ConditionalRule<>))]
    public class When_the_condition_is_not_met_for_the_rule : ContextBase<ConditionalRule<ItemToValidate>>
    {
        private bool _isValid;

        protected override ConditionalRule<ItemToValidate> CreateSut()
        {
            return new ConditionalRule<ItemToValidate>(entity => false, new Rule<ItemToValidate>(entity => true, "xxx"));
        }

        protected override void When()
        {
            _isValid = Sut.IsValid(new ItemToValidate());
        }

        [Observation]
        public void Then_the_entity_should_be_valid()
        {
            _isValid.Should().BeTrue();
        }
    }

    [Concern(typeof(ConditionalRule<>))]
    public class When_the_condition_is_met_for_the_rule_and_the_inner_rule_is_valid : ContextBase<ConditionalRule<ItemToValidate>>
    {
        private bool _isValid;

        protected override ConditionalRule<ItemToValidate> CreateSut()
        {
            return new ConditionalRule<ItemToValidate>(entity => true, new Rule<ItemToValidate>(entity => true, "xxx"));
        }

        protected override void When()
        {
            _isValid = Sut.IsValid(new ItemToValidate());
        }

        [Observation]
        public void Then_the_entity_should_be_valid()
        {
            _isValid.Should().BeTrue();
        }
    }

    [Concern(typeof(ConditionalRule<>))]
    public class When_the_condition_is_met_for_the_rule_and_the_inner_rule_is_not_valid : ContextBase<ConditionalRule<ItemToValidate>>
    {
        private bool _isValid;

        protected override ConditionalRule<ItemToValidate> CreateSut()
        {
            return new ConditionalRule<ItemToValidate>(entity => true, new Rule<ItemToValidate>(entity => false, "xxx"));
        }

        protected override void When()
        {
            _isValid = Sut.IsValid(new ItemToValidate());
        }

        [Observation]
        public void Then_the_entity_should_not_be_valid()
        {
            _isValid.Should().BeFalse();
        }
    }
}