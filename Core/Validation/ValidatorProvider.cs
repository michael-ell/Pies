using System;
using System.Collections.Generic;

namespace Codell.Pies.Core.Validation
{
    public class ValidatorProvider : IValidatorProvider
    {
        private readonly IDictionary<Type, IValidator> _inner;

        public ValidatorProvider()
        {
            _inner = new Dictionary<Type, IValidator>();
        }

        public void Register(Type type, IValidator validator)
        {
            if (validator != null)
                _inner[type] = validator;
        }

        public bool HasValidatorFor(Type type)
        {
            return _inner.ContainsKey(type);
        }

        public IValidator ValidatorFor(Type type)
        {
            IValidator validator;
            _inner.TryGetValue(type, out validator);
            return validator ?? new NullValidator();
        }
    }
}