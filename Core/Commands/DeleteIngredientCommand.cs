using System;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "DeleteIngredient")]
    public class DeleteIngredientCommand : CommandBase
    {
        public Guid Id { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public DeleteIngredientCommand(Guid ingredientId, Guid pieId)
        {
            Id = ingredientId;
            PieId = pieId;
        }
    }
}