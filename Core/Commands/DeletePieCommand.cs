using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "Delete")]
    public class DeletePieCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public DeletePieCommand(Guid id)
        {
            Id = id;
        }
    }
}