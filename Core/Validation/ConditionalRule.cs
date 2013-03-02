using System;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class ConditionalRule<T> : RuleBase<T>
    {
        private readonly Func<T, bool> _condition;
        private readonly IRule<T> _inner;

        public ConditionalRule(Func<T, bool> condition, IRule<T> inner)
        {
            Verify.NotNull(condition, "condition");
            Verify.NotNull(inner, "inner");
            
            _condition = condition;
            _inner = inner;
        }

        public override bool IsValid(T entity)
        {
            return !_condition.Invoke(entity) || _inner.IsValid(entity);
        }

        public override string ErrorMessage { get { return _inner.ErrorMessage; } }
    }
}