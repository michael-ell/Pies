using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;
using Codell.Pies.Testing.Creators.Helpers;

namespace Codell.Pies.Tests.Core.Validation.ValidRuleSpecs
{
    [Concern(typeof(ValidRule<object>))]
    public class When_creating_a_valid_rule : ContextBase<ValidRule<object>>
    {
        private object _entity;

        protected override ValidRule<object> CreateSut()
        {
            _entity = new object();
            return new ValidRule<object>(_entity);
        }

        protected override void When()
        {
        }

        [Observation]
        public void Then_the_rule_should_be_valid()
        {
            Sut.IsValid.Should().BeTrue();
        }

        [Observation]
        public void Then_the_entity_should_available()
        {
            Sut.Entity.Should().Be(_entity);
        }
    }

    [Concern(typeof(ValidRule<object>))]
    public class When_creating_many_valid_rules : ContextBase
    {
        private IEnumerable<object> _validEntities;
        private IEnumerable<IValidatedRule<object>> _rules;

        protected override void Given()
        {
            _validEntities = New.List<object>().Fill(10);
        }

        protected override void When()
        {
            _rules = ValidRule<object>.Create(_validEntities);
        }

        [Observation]
        public void Then_should_create_the_correct_number_of_valid_rules()
        {
            _rules.Count().Should().Be(_validEntities.Count());
        }
    }
}