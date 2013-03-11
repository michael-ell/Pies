using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "DeleteSlice")]
    public class DeleteSliceCommand : CommandBase
    {
        public Guid SliceId { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public DeleteSliceCommand(Guid sliceId, Guid pieId)
        {
            SliceId = sliceId;
            PieId = pieId;
        }
    }
}