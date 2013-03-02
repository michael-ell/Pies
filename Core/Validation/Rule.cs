using System;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class Rule<T> : RuleBase<T>
    {
        private readonly Predicate<T> _isValidPredicate;
        private string _errorMessage;
        private readonly Func<T, string> _errorMessageProvider;

        public Rule(Predicate<T> isValidPredicate, string errorMessage)
        {
            Verify.NotNull(isValidPredicate, "isValidPredicate");
            Verify.NotWhitespace(errorMessage, "errorMessage");

            _isValidPredicate = isValidPredicate;
            _errorMessage = errorMessage;
        }

        public Rule(Predicate<T> isValidPredicate, Func<T, string> errorMessageProvider)
        {
            Verify.NotNull(isValidPredicate, "isValidPredicate");
            Verify.NotNull(errorMessageProvider, "errorMessageProvider");

            _isValidPredicate = isValidPredicate;
            _errorMessageProvider = errorMessageProvider;
        }

        public override bool IsValid(T entity)
        {
            var isValid = _isValidPredicate.Invoke(entity);
            if (!isValid && _errorMessageProvider != null)
                _errorMessage = _errorMessageProvider.Invoke(entity);
            return isValid;
        }

        public override string ErrorMessage
        {
            get { return _errorMessage; }
        }
    }
}