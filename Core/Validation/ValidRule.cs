using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class ValidRule<T> : IValidatedRule<T>
    {
        public static IEnumerable<IValidatedRule<T>> Create(IEnumerable<T> entities)
        {
            return entities.IsEmpty() ? new List<IValidatedRule<T>>() : entities.Select(entity => new ValidRule<T>(entity)).Cast<IValidatedRule<T>>();
        }

        public ValidRule(T entity)
        {
            Entity = entity;
        }

        public bool IsValid
        {
            get { return true; }
        }

        public T Entity { get; private set; }

        public string ErrorMessage { get { return null; } }
    }
}