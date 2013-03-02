using System;

namespace Codell.Pies.Core.Validation
{
    public interface IValidatorProvider
    {
        void Register(Type type, IValidator validator);
        bool HasValidatorFor(Type type);
        IValidator ValidatorFor(Type type);
    }
}