using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.ValueTypes;

namespace Codell.Pies.Core.Cqrs
{
    public class CommandResult
    {
        private readonly List<BrokenRule> _brokenRules;

        public CommandResult()
        {
            _brokenRules = new List<BrokenRule>();
        }

        public void Add(IEnumerable<BrokenRule> brokenRules)
        {
            if (brokenRules.IsEmpty()) return;
            _brokenRules.AddRange(brokenRules);
        }

        public CommandResult Add(BrokenRule brokenRule)
        {
            if (brokenRule != null)
                _brokenRules.Add(brokenRule);
            return this;
        }

        public CommandResult Add(CommandResult result)
        {
            if (result != null)
                _brokenRules.AddRange(result.BrokenRules);
            return this;
        }

        public CommandResult Add(IEnumerable<string> brokenRules)
        {
            if (brokenRules.IsNotEmpty())
                _brokenRules.AddRange(brokenRules.Select(message => new BrokenRule(message)).ToList());
            return this;
        }

        public IEnumerable<BrokenRule> BrokenRules
        {
            get { return _brokenRules; }
        }
    }

    public class CommandResult<TContext> where TContext : class
    {
        private readonly List<ContextualBrokenRules<TContext>> _brokenRules;

        public CommandResult()
        {        
            _brokenRules = new List<ContextualBrokenRules<TContext>>();
        }

        public IEnumerable<ContextualBrokenRules<TContext>> BrokenRules
        {
            get { return _brokenRules; }
        }

        public CommandResult<TContext> Add(Validation.ContextualBrokenRules<TContext> brokenRule)
        {
            _brokenRules.Add(new ContextualBrokenRules<TContext>(brokenRule.Context, brokenRule.Messages));
            return this;
        }

        public CommandResult<TContext> Add(Validation.ContextualBrokenRules<IEnumerable<TContext>> brokenRule)
        {
            _brokenRules.Add(new ContextualBrokenRules<TContext>(null, brokenRule.Messages));
            return this;
        }

        public CommandResult<TContext> Add(IEnumerable<string> brokenRules)
        {
            _brokenRules.Add(new ContextualBrokenRules<TContext>(null, brokenRules));
            return this;
        }
    }
}