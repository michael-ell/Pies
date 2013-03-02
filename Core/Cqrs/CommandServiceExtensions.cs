using System;
using System.Collections.Generic;
using System.Reflection;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Core.Cqrs
{
    public static class CommandServiceExtensions
    {
       public static CommandResult Run<T>(this ICommandService service, CommandBase command, IBrokenRuleInspector<T> ruleInspector) where T :BusinessRuleException
        {
            var result = new CommandResult();
            if (service == null || command == null) return result;
            try
            {
                service.Execute(command);
            }
            catch (TargetInvocationException e)
            {
                if (ruleInspector != null) ruleInspector.Run(e.InnerException as T);
                var inner = e.InnerException as BusinessRuleException;
                if (inner != null)
                {
                    result.Add(inner.BrokenRules);
                }
                else
                {
                    throw;
                }
            }
            catch (BusinessRuleException e)
            {
                if(ruleInspector != null) ruleInspector.Run((T)e);
                result.Add(e.BrokenRules);
            }
            return result;
        }

        public static CommandResult Run(this ICommandService service, CommandBase command)
        {
            return Run<BusinessRuleException>(service, command, null);
        }

        public static CommandResult<TBrokenRuleContext> Run<TBrokenRuleContext>(this ICommandService service, CommandBase command) where TBrokenRuleContext : class
        {
            var result = new CommandResult<TBrokenRuleContext>();
            if (service == null || command == null) return result;
            try
            {
                service.Execute(command);
            }
            catch (TargetInvocationException e)
            {
                var singleException = e.InnerException as BusinessRuleException<TBrokenRuleContext>;
                if (singleException != null)
                {
                    foreach (var brokenRule in singleException.BrokenRules)
                    {
                        result.Add(brokenRule);
                    }
                }
                else
                {
                    var groupException = e.InnerException as BusinessRuleException<IEnumerable<TBrokenRuleContext>>;
                    if (groupException != null)
                    {
                        foreach (var brokenRule in groupException.BrokenRules)
                        {
                            result.Add(brokenRule);
                        }
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            catch (BusinessRuleException<TBrokenRuleContext> e)
            {
                foreach (var brokenRule in e.BrokenRules)
                {
                    result.Add(brokenRule);
                }
            }
            return result;
        }
    }

     public class BrokenRuleInspector<T> : IBrokenRuleInspector<T> where T : BusinessRuleException
     {
        private readonly Func<T, bool> _ruleInspector;

        public BrokenRuleInspector(Func<T, bool> ruleInspector)
        {
            _ruleInspector = ruleInspector;
        }

        public void Run(T exception)
        {
            IsMatch = _ruleInspector.Invoke(exception);
        }

         public Boolean IsMatch { get; private set; }
     }

    public interface IBrokenRuleInspector<T> where T : BusinessRuleException
    {
        void Run(T exception);
        Boolean IsMatch { get; }
    }
}