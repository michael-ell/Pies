using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class ValidationResult : IValidationResult
    {
        private readonly List<string> _brokenRuleMessages;

        public ValidationResult()
        {
            _brokenRuleMessages = new List<string>();
        }

        public bool HasBrokenRules
        {
            get { return _brokenRuleMessages.Count > 0; }
        }

        public void Add(string brokenRule)
        {
            if (brokenRule.IsNotEmpty())
            {
                _brokenRuleMessages.Add(brokenRule);
            }
        }

        public void Add(IValidationResult result)
        {
            if (result != null && result.HasBrokenRules)
                _brokenRuleMessages.AddRange(result.DistinctBrokenRules);
        }

        public IEnumerable<string> DistinctBrokenRules
        {
            get { return _brokenRuleMessages.Distinct().ToList(); }
        }
    }

    public class ValidationResult<T> : IValidationResult<T>
    {
        private readonly HashSet<IValidatedRule<T>> _validatedRules;
        private readonly HashSet<IValidatedRule<IEnumerable<T>>> _validatedGroupRules;
        private readonly HashSet<T> _validEntities;
        private readonly HashSet<T> _invalidEntities;
        private readonly List<string> _externalBrokenRuleMessages;

        public ValidationResult()
        {
            _validatedRules = new HashSet<IValidatedRule<T>>();
            _validatedGroupRules = new HashSet<IValidatedRule<IEnumerable<T>>>();
            _validEntities = new HashSet<T>();
            _invalidEntities = new HashSet<T>();
            _externalBrokenRuleMessages = new List<string>();
        }

        public bool HasBrokenRules { get { return InvalidGroupRules.Any() || InvalidRules.Any(); } }

        public bool HasBrokenGroupRules { get { return InvalidGroupRules.Any(); } }

        public IEnumerable<IValidatedRule<T>> ValidatedRules
        {
            get { return _validatedRules; }
        }

        public IEnumerable<ContextualBrokenRules<T>> BrokenRules
        {
            get
            {
                var brokenRulesByEntity = InvalidRules.GroupBy(rule => rule.Entity);
                return brokenRulesByEntity.Select(@group => new ContextualBrokenRules<T>(@group.Key, 
                                                                                         @group.Select(brokenRule => brokenRule.ErrorMessage)
                                          .ToList()));
            }
        }

        public IEnumerable<ContextualBrokenRules<IEnumerable<T>>> BrokenGroupRules
        {
            get
            {
                var brokenRulesByGroups = InvalidGroupRules.GroupBy(rule => rule.Entity);
                return brokenRulesByGroups.Select(@group => new ContextualBrokenRules<IEnumerable<T>>(@group.Key, 
                                                                                                      @group.Select(brokenRule => brokenRule.ErrorMessage)
                                          .ToList()));
            }
        }

        public IEnumerable<T> ValidEntities
        {
            get { return _validEntities.Where(entity => !InvalidEntities.Contains(entity)).ToList(); }
        }

        public IEnumerable<T> InvalidEntities
        {
            get { return _invalidEntities; }
        }

        private IEnumerable<IValidatedRule<T>> InvalidRules
        {
            get { return _validatedRules.Where(rule => !rule.IsValid); }
        }

        private IEnumerable<IValidatedRule<IEnumerable<T>>> InvalidGroupRules
        {
            get { return _validatedGroupRules.Where(rule => !rule.IsValid); }
        }

        public IEnumerable<string> DistinctBrokenRules
        {
            get
            {
                var messages = InvalidRules.Select(rule => rule.ErrorMessage).ToList();
                messages.AddRange(_externalBrokenRuleMessages);
                return messages.Distinct().ToList();
            }
        }

        public void Add(IValidatedRule<T> validatedRule)
        {
            _validatedRules.Add(validatedRule);
            if (validatedRule.IsValid)
            {
                _validEntities.Add(validatedRule.Entity);
            }
            else
            {
                _invalidEntities.Add(validatedRule.Entity);
            }
        }

        public void Add(IValidatedRule<IEnumerable<T>> validatedRule)
        {
            _validatedGroupRules.Add(validatedRule);
        }

        public void Add(IValidationResult result)
        {
            if (result != null && result.HasBrokenRules)
            {
                _externalBrokenRuleMessages.AddRange(result.DistinctBrokenRules);
            }
        }

        public void Add(IEnumerable<IValidatedRule<T>> validatedRules)
        {
            if (validatedRules == null) return;
            foreach (var validatedRule in validatedRules)
                Add(validatedRule);
        }

        public void Add(IValidationResult<T> result)
        {
            if (result == null) return;
            foreach (var validatedRule in result.ValidatedRules)
                Add(validatedRule);
        }
    }
}