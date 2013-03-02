using System.Collections.Generic;

namespace Codell.Pies.Core.ValueTypes
{
    public class ContextualBrokenRules<TContext> where TContext : class
    {
        public ContextualBrokenRules(TContext context, IEnumerable<string> messages)
        {
            Context = context;
            Messages = messages ?? new List<string>();
        }

        public TContext Context { get; private set; }

        public IEnumerable<string> Messages { get; private set; }
    }
}