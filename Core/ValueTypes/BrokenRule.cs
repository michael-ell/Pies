using Codell.Pies.Common;

namespace Codell.Pies.Core.ValueTypes
{
    public class BrokenRule
    {
        public BrokenRule(string message) : this(message, Severity.Unknown)
        {
        }

        public BrokenRule(string message, Severity severity)
        {
            Verify.NotWhitespace(message, "message");
            Message = message;
            Severity = severity;
        }

        public string Message { get; private set; }

        public Severity Severity { get; private set; }
    }
}