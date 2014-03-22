using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateIsPrivate")]
    public class UpdateIsPrivateCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public bool IsPrivate { get; private set; }

        public UpdateIsPrivateCommand(Guid id, bool isPrivate)
        {
            Id = id;
            IsPrivate = isPrivate;
        }
    }
}