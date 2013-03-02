using System.Collections.Generic;

namespace Codell.Pies.Core.Validation
{
    public interface IValidationResult
    {
        bool HasBrokenRules { get; }
        void Add(IValidationResult result);
        IEnumerable<string> DistinctBrokenRules { get; }
    }

    public interface IValidationResult<T> : IValidationResult
    {
        bool HasBrokenGroupRules { get; }

        IEnumerable<T> ValidEntities { get; }

        IEnumerable<T> InvalidEntities { get; }

        IEnumerable<IValidatedRule<T>> ValidatedRules { get; }

        IEnumerable<ContextualBrokenRules<T>> BrokenRules { get; }
        IEnumerable<ContextualBrokenRules<IEnumerable<T>>> BrokenGroupRules { get; }

        void Add(IValidationResult<T> result);
    }
}