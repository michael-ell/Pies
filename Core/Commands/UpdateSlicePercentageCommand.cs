using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateSlicePercentage")]
    public class UpdateSlicePercentageCommand : CommandBase
    {
        public Guid SliceId { get; private set; }

        public int ProposedPercent { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public UpdateSlicePercentageCommand(Guid sliceId, int proposedPercent, Guid pieId)
        {
            SliceId = sliceId;
            ProposedPercent = proposedPercent;
            PieId = pieId;
        }
    }
}