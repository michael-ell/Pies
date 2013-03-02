using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class RulesValidator : IValidator
    {
        private readonly List<IRule> _rules;

        public RulesValidator() : this(new List<IRule>())
        {
        }

        public RulesValidator(IEnumerable<IRule> rules)
        {
            _rules = rules == null ? new List<IRule>() : rules.ToList();
        }

        public void Add(params IRule[] rules)
        {
            if (rules != null)
            {
                _rules.AddRange(rules);
            }
        }

        public IValidationResult Validate(object entity)
        {
            var result = new ValidationResult();
            foreach (var rule in _rules)
            {
                if (!rule.IsValid(entity))
                {
                    result.Add(rule.ErrorMessage);
                }
            }
            return result;
        }
    }

    public class RulesValidator<T> : IValidator<T>
    {
        private readonly List<IRule<T>> _rules;
        private readonly List<IRule<IEnumerable<T>>> _groupRules;

        public RulesValidator()
        {
            _rules = new List<IRule<T>>();
            _groupRules = new List<IRule<IEnumerable<T>>>();
        }

        public RulesValidator(IEnumerable<IRule<T>> rules)
        {
            Verify.NotNull(rules, "rules");            
            _rules = new List<IRule<T>>(rules);
        }

        public RulesValidator<T> Add(IRule<T> rule)
        {
            _rules.Add(rule);
            return this;
        }

        public RulesValidator<T> Add(IRule<IEnumerable<T>> rule)
        {
            _groupRules.Add(rule);
            return this;
        }

        public RulesValidator<T> Add(IEnumerable<IRule<T>> rules)
        {
            if (rules.IsNotEmpty())
            {
                _rules.AddRange(rules);
            }
            return this;    
        }

        public IEnumerable<IRule<T>> Rules
        {
            get { return _rules; }
        }

        public IValidationResult<T> Validate(IEnumerable<T> entities)
        {
            var result = ValidateAgainstGroupRules(entities);
            if (result.HasBrokenGroupRules) return result;

            foreach (var entity in entities)
                result.Add(Validate(entity));
            return result;
        }

        private IValidationResult<T> ValidateAgainstGroupRules(IEnumerable<T> entities)
        {
            var result = new ValidationResult<T>();
            foreach (var groupRule in _groupRules)
            {
                if (groupRule.IsValid(entities))
                {
                    result.Add(new ValidRule<IEnumerable<T>>(entities));
                }
                else
                {
                    result.Add(new InvalidRule<IEnumerable<T>>(entities, groupRule.ErrorMessage));
                    if (groupRule.StopValidatingIfBroken)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public IValidationResult<T> Validate(T entity)
        {          
            var result = new ValidationResult<T>();            
            foreach (var rule in _rules)
            {
                if (rule.IsValid(entity))
                {
                    result.Add(new ValidRule<T>(entity));
                }
                else
                {
                    result.Add(new InvalidRule<T>(entity, rule.ErrorMessage));
                    if (rule.StopValidatingIfBroken)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public IValidationResult Validate(object entity)
        {
            return Validate((T)entity);
        }
    }
}