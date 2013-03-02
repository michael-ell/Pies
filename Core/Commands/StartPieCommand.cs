using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootConstructor(typeof(Pie))]
    public class StartPieCommand : CommandBase
    {
        public Guid Id { get; private set; }

        public StartPieCommand(Guid id)
        {
            Id = id;
        }
    }
}