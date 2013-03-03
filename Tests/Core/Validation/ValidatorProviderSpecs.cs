using Codell.Pies.Core.Validation;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Validation.ValidatorProviderSpecs
{
    [Concern(typeof (ValidatorProvider))]
    public class When_a_validator_is_registered_for_a_type : ContextBase<ValidatorProvider>
    {
        private IValidator _mockValidator;

        protected override void Given()
        {
            _mockValidator = GetDependency<IValidator>();
        }

        protected override void When()
        {
            Sut.Register(typeof(ItemToValidate), _mockValidator);      
        }

        [Observation]
        public void Then_should_indicate_that_it_can_provide_a_validator()
        {
            Sut.HasValidatorFor(typeof (ItemToValidate)).Should().BeTrue();
        }

        [Observation]
        public void Then_should_return_the_validator_when_requested()
        {
            Sut.ValidatorFor(typeof(ItemToValidate)).Should().Be(_mockValidator);
        }
    }

    [Concern(typeof(ValidatorProvider))]
    public class When_a_validator_is_not_registered_for_a_type : ContextBase<ValidatorProvider>
    {

        protected override void When()
        {
            //na
        }

        [Observation]
        public void Then_should_indicate_that_it_cannot_provide_a_validator()
        {
            Sut.HasValidatorFor(typeof(ItemToValidate)).Should().BeFalse();
        }

        [Observation]
        public void Then_should_return_a_null_validator_when_requested()
        {
            Sut.ValidatorFor(typeof (ItemToValidate)).Should().NotBeNull();
            Sut.ValidatorFor(typeof (ItemToValidate)).Should().BeOfType<NullValidator>();
        }
    }
}