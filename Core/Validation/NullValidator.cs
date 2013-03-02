namespace Codell.Pies.Core.Validation
{
    public class NullValidator : IValidator
    {
        public IValidationResult Validate(object entity)
        {
            return new ValidationResult();
        }
    }
}