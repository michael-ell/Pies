using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class InvalidRule<T> : IValidatedRule<T>
    {
        public static IEnumerable<IValidatedRule<T>> Create(IEnumerable<T> entities, string errorMessage)
        {
            return entities.IsEmpty() ? new List<IValidatedRule<T>>() : entities.Select(entity => new InvalidRule<T>(entity, errorMessage)).Cast<IValidatedRule<T>>();
        }

        public static IEnumerable<IValidatedRule<T>> Create(IEnumerable<T> entities, IErrorProvider<T> errorProvider)
        {
            return entities.IsEmpty() ? new List<IValidatedRule<T>>() : entities.Select(entity => new InvalidRule<T>(entity, errorProvider.GetErrorMessageFor(entity))).Cast<IValidatedRule<T>>();
        }

        public InvalidRule(T entity, string errorMessage)
        {
            Verify.NotWhitespace(errorMessage, "errorMessage");            
            ErrorMessage = errorMessage;
            Entity = entity;
        }

        public bool IsValid
        {
            get { return false; }
        }

        public T Entity { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}