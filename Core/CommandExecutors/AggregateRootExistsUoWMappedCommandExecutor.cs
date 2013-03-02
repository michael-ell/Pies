using System;
using System.Reflection;
using Codell.Pies.Core.Domain;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping;
using Ncqrs.Domain;

namespace Codell.Pies.Core.CommandExecutors
{
    public class AggregateRootExistsUoWMappedCommandExecutor : CommandExecutorBase<ICommand>
    {
        private readonly ICommandMapper _commandMapper;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AggregateRootExistsUoWMappedCommandExecutor(ICommandMapper commandMapper)
        {
            _commandMapper = commandMapper;
        }

        protected override void ExecuteInContext(IUnitOfWorkContext context, ICommand command)
        {
            _commandMapper.Map(command, new UoWMappedCommandExecutorCallbacks(context, command));
        }

        private class UoWMappedCommandExecutorCallbacks : IMappedCommandExecutor
        {
            private readonly IUnitOfWorkContext _uow;
            private readonly ICommand _command;

            public UoWMappedCommandExecutorCallbacks(IUnitOfWorkContext uow, ICommand command)
            {
                _uow = uow;
                _command = command;
            }

            public void ExecuteActionOnExistingInstance(Func<ICommand, Guid> idCallback, Func<ICommand, Type> typeCallback, Action<AggregateRoot, ICommand> action)
            {
                var id = idCallback(_command);
                var type = typeCallback(_command);
                var aggRoot = _uow.GetById(type, id, _command.KnownVersion);
                if (aggRoot == null)
                {
                    Log.Warn(string.Format("Could not find aggregate root {0}, by id {1}", type.Name, id));
                }
                else
                {
                    try
                    {
                        action(aggRoot, _command);
                        _uow.Accept();
                    }
                    catch(TargetInvocationException e)
                    {
                        var inner = e.InnerException as BusinessRuleException;
                        if (inner != null)
                        {
                            _uow.Accept();
                        }
                        throw;
                    }
                    catch (BusinessRuleException)
                    {
                        _uow.Accept();
                        throw;
                    }
                }
            }

            public void ExecuteActionOnNewInstance(Action<AggregateRoot, ICommand> action)
            {
                throw new NotSupportedException();
            }

            public void ExecuteActionCreatingNewInstance(Func<ICommand, AggregateRoot> action)
            {
                action(_command);
                _uow.Accept();
            }

            public void ExecuteActionOnExistingOrCreatingNewInstance(Func<ICommand, Guid> idCallback, Func<ICommand, Type> typeCallback, Action<AggregateRoot, ICommand> existingAction, Func<ICommand, AggregateRoot> creatingAction)
            {
                var id = idCallback(_command);
                var type = typeCallback(_command);
                var aggRoot = _uow.GetById(type, id, _command.KnownVersion);

                if (aggRoot == null)
                {
                    creatingAction(_command);
                    _uow.Accept();
                }
                else
                {
                    existingAction(aggRoot, _command);
                    _uow.Accept();
                }
            }
        }
    }
}