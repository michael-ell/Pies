using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateIngredientPercentage")]
    public class UpdateIngredientPercentageCommand : CommandBase
    {
        public Guid Id { get; private set; }

        public int ProposedPercent { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public UpdateIngredientPercentageCommand(Guid id, int proposedPercent, Guid pieId)
        {
            Id = id;
            ProposedPercent = proposedPercent;
            PieId = pieId;
        }
    }
}