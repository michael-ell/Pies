using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Creators.Helpers;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Validation.InvalidRuleSpecs
{
    [Concern(typeof (InvalidRule<object>))]
    public class When_creating_an_invalid_rule : ContextBase<InvalidRule<object>>
    {
        private object _entity;
        private string _errorMessage;

        protected override InvalidRule<object> CreateSut()
        {
            _entity = new object();
            _errorMessage = "some error message";
            return new InvalidRule<object>(_entity, _errorMessage);
        }

        protected override void When()
        {            
        }

        [Observation]
        public void Then_the_rule_should_not_be_valid()
        {
            Sut.IsValid.Should().BeFalse();
        }

        [Observation]
        public void Then_the_proper_error_message_should_available()
        {
            Sut.ErrorMessage.Should().Be(_errorMessage);
        }

        [Observation]
        public void Then_the_entity_should_available()
        {
            Sut.Entity.Should().Be(_entity);
        }
    }

    [Concern(typeof (InvalidRule<object>))]
    public class When_creating_many_invalid_rules : ContextBase
    {
        private IEnumerable<object> _invalidEntities;
        private IEnumerable<IValidatedRule<object>> _rules;

        protected override void Given()
        {
            _invalidEntities = New.List<object>().Fill(10);
        }

        protected override void When()
        {
            _rules = InvalidRule<object>.Create(_invalidEntities, "some error message");
        }

        [Observation]
        public void Then_should_create_the_correct_number_of_invalid_rules()
        {
            _rules.Count().Should().Be(_invalidEntities.Count());
        }
    }
}