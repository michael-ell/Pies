using System.Collections.Generic;

namespace Codell.Pies.Core.Validation
{
    public class ContextualBrokenRules<TContext>
    {
        public ContextualBrokenRules(TContext context, IEnumerable<string> brokenRules)
        {
            Context = context;
            Messages = brokenRules;
        }

        public TContext Context { get; private set; }

        public IEnumerable<string> Messages { get; private set; } 
    }
}