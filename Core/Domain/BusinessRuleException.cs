using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Core.ValueTypes;

namespace Codell.Pies.Core.Domain
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : this(message, Severity.Unknown)
        {
        }

        public BusinessRuleException(IEnumerable<string> messages, Severity severity) : base(messages.Aggregate((m1, m2) => m1 + "\r\n" + m2))
        {
            Verify.NotNull(messages, "messages");
            BrokenRules = messages.Select( m => new BrokenRule(m, severity) );
        }

        public BusinessRuleException(string message, Severity severity) : base(message)
        {
            Verify.NotWhitespace(message, "message");
            BrokenRules = new List<BrokenRule> { new BrokenRule(message, severity) };
        }


        public BusinessRuleException(IEnumerable<string> brokenRules)
        {
            Verify.NotNull(brokenRules, "brokenRules");
            BrokenRules = brokenRules.Select(rule => new BrokenRule(rule)).ToList();
        }

        public IEnumerable<BrokenRule> BrokenRules { get; private set; }
    }

    public class BusinessRuleException<TContext> : Exception where TContext : class 
    {
        public BusinessRuleException(Validation.ContextualBrokenRules<TContext> brokenRule)
        {
            BrokenRules = brokenRule == null ? new List<Validation.ContextualBrokenRules<TContext>>() : new List<Validation.ContextualBrokenRules<TContext>>{brokenRule};
        }

        public BusinessRuleException(IEnumerable<Validation.ContextualBrokenRules<TContext>> brokenRules)
        {
            BrokenRules = brokenRules ?? new List<Validation.ContextualBrokenRules<TContext>>();
        }

        public IEnumerable<Validation.ContextualBrokenRules<TContext>> BrokenRules { get; private set; }
    }
}