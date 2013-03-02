using System;
using System.Text.RegularExpressions;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class RegularExpressionRule<T> : RuleBase<T>
    {
        private readonly Func<T, string> _function;
        private readonly String _regularExpression;
        private readonly String _errorMessage;

        public RegularExpressionRule(Func<T, string> function, String regularExpression, String errorMessage)
        {
            Verify.NotNull(function, "function");
            Verify.NotNull(regularExpression, "regularExpression");
            Verify.NotNull(errorMessage, "errorMessage");

            _function = function;
            _regularExpression = regularExpression;
            _errorMessage = errorMessage;
        }

        public override bool IsValid(T entity)
        {
            var stringToEvaluate = _function.Invoke(entity) ?? string.Empty;
            return Regex.IsMatch(stringToEvaluate, _regularExpression);
        }

        public override string ErrorMessage
        {
            get { return _errorMessage; }
        }
    }
}