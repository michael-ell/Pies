using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;
using Moq;

namespace Codell.Pies.Tests.Core.Validation.RulesValidatorSpecs
{
    [Concern(typeof (RulesValidator<object>))]
    public class When_validating_an_entity_against_an_rule_that_passes : ContextBase<RulesValidator<ItemToValidate>>
    {
        private ItemToValidate _someEntity;
        private Mock<IRule<ItemToValidate>> _mockRule;
        private IValidationResult<ItemToValidate> _results;

        protected override RulesValidator<ItemToValidate> CreateSut()
        {
            _mockRule = GetMock<IRule<ItemToValidate>>();
            RegisterDependency<IEnumerable<IRule<ItemToValidate>>>(new List<IRule<ItemToValidate>> { _mockRule.Object });
            return base.CreateSut();
        }

        protected override void Given()
        {
            _someEntity = new ItemToValidate();
            _mockRule.Setup(rule => rule.IsValid(_someEntity)).Returns(true);            
        }

        protected override void When()
        {
            _results = Sut.Validate(_someEntity);
        }

        [Observation]
        public void Then_the_rule_should_be_valid()
        {
            _results.ValidEntities.Should().Contain(_someEntity);
        }
    }

    [Concern(typeof(RulesValidator<object>))]
    public class When_validating_an_entity_against_a_rule_that_fails : ContextBase<RulesValidator<ItemToValidate>>
    {
        private ItemToValidate _someEntity;
        private Mock<IRule<ItemToValidate>> _mockRule;
        private IValidationResult<ItemToValidate> _results;
        private string _errorMessage;

        protected override RulesValidator<ItemToValidate> CreateSut()
        {
            _mockRule = GetMock<IRule<ItemToValidate>>();
            RegisterDependency<IEnumerable<IRule<ItemToValidate>>>(new List<IRule<ItemToValidate>> { _mockRule.Object });
            return base.CreateSut();
        }

        protected override void Given()
        {
            _someEntity = new ItemToValidate();
            _errorMessage = "some error message";
            _mockRule.Setup(rule => rule.IsValid(_someEntity)).Returns(false);
            _mockRule.Setup(rule => rule.ErrorMessage).Returns(_errorMessage);
        }

        protected override void When()
        {
            _results = Sut.Validate(_someEntity);
        }

        [Observation]
        public void Then_the_rule_should_be_invalid()
        {
            _results.InvalidEntities.Should().Contain(_someEntity);
        }

        [Observation]
        public void Then_error_message_from_the_rule_should_accessible_from_the_validated_rule()
        {
            _results.DistinctBrokenRules.SingleOrDefault(message => message == _errorMessage).Should().NotBeNull();
        }
    }

    [Concern(typeof (RulesValidator<object>))]
    public class When_validating_against_many_rules : ContextBase<RulesValidator<ItemToValidate>>
    {
        private ItemToValidate _someEntity;
        private Mock<IRule<ItemToValidate>> _mockRule;
        private List<IRule<ItemToValidate>> _rules;

        protected override RulesValidator<ItemToValidate> CreateSut()
        {
            _mockRule = GetMock<IRule<ItemToValidate>>();
            _rules = new List<IRule<ItemToValidate>> { _mockRule.Object, _mockRule.Object };
            RegisterDependency<IEnumerable<IRule<ItemToValidate>>>(_rules);
            return base.CreateSut();
        }

        protected override void Given()
        {
            _someEntity = new ItemToValidate();
            _mockRule.Setup(rule => rule.IsValid(_someEntity)).Returns(true).AtMost(2);
        }

        protected override void When()
        {
            Sut.Validate(_someEntity);
        }

        [Observation]
        public void Then_should_validate_each_rule_against_the_entity()
        {
            _mockRule.Verify(rule => rule.IsValid(_someEntity), Times.Exactly(2));
        }
    }

    [Concern(typeof (RulesValidator))]
    public class When_using_an_untyped_validator_and_an_entity_is_valid : ContextBase<RulesValidator>
    {
        private IValidationResult _result;
        private object _entity;

        protected override RulesValidator CreateSut()
        {
            return new RulesValidator();
        }

        protected override void Given()
        {
            _entity = new ItemToValidate();
            MockFor<IRule>().Setup(rule => rule.IsValid(_entity)).Returns(true);
            Sut.Add(GetDependency<IRule>());
        }

        protected override void When()
        {
            _result = Sut.Validate(_entity);
        }

        [Observation]
        public void Then_should_not_have_any_broken_rules()
        {
            _result.HasBrokenRules.Should().BeFalse();
        }
    }

    [Concern(typeof(RulesValidator))]
    public class When_using_an_untyped_validator_and_an_entity_is_not_valid : ContextBase<RulesValidator>
    {
        private IValidationResult _result;
        private object _entity;
        private string _expectedMessage;

        protected override RulesValidator CreateSut()
        {
            return new RulesValidator();
        }

        protected override void Given()
        {
            _entity = new ItemToValidate();
            _expectedMessage = "some message";
            MockFor<IRule>().Setup(rule => rule.IsValid(_entity)).Returns(false);
            MockFor<IRule>().Setup(rule => rule.ErrorMessage).Returns(_expectedMessage);
            Sut.Add(GetDependency<IRule>());
        }

        protected override void When()
        {
            _result = Sut.Validate(_entity);
        }

        [Observation]
        public void Then_should_have_broken_rules()
        {
            _result.HasBrokenRules.Should().BeTrue();
        }

        [Observation]
        public void Then_should_have_the_broken_rule_message()
        {
            _result.DistinctBrokenRules.Should().Contain(_expectedMessage);
        }
    }
}