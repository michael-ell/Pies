using System.Collections.Generic;

namespace Codell.Pies.Core.Validation
{
    public interface IValidator
    {
        IValidationResult Validate(object entity);
    }

    public interface IValidator<T> : IValidator
    {
        IValidationResult<T> Validate(IEnumerable<T> entities);
        IValidationResult<T> Validate(T entity);
    }
}