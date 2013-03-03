using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Validation.RuleSpecs
{
    [Concern(typeof (Rule<>))]
    public class When_the_rule_is_not_broken_based_off_the_predicate : ContextBase<Rule<string>>
    {
        protected override Rule<string> CreateSut()
        {
            return new Rule<string>(s => s.Length > 0, "some error message");
        }

        protected override void When()
        {
        }

        [Observation]
        public void Then_the_rule_should_be_valid()
        {
            Sut.IsValid("should be valid").Should().BeTrue();
        }
    }

    [Concern(typeof(Rule<>))]
    public class When_the_rule_is_broken_based_off_the_predicate : ContextBase<Rule<string>>
    {
        private string _expectedErrorMessage;

        protected override Rule<string> CreateSut()
        {
            _expectedErrorMessage = "some error message";
            return new Rule<string>(s => s.Length > 0, _expectedErrorMessage);
        }

        protected override void When()
        {
        }

        [Observation]
        public void Then_the_rule_should_be_invalid()
        {
            Sut.IsValid(string.Empty).Should().BeFalse();
        }

        [Observation]
        public void Then_should_get_the_correct_error_message()
        {
            Sut.ErrorMessage.Should().Be(_expectedErrorMessage);
        }
    }
}