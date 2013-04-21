using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateIngredientColor")]
    public class UpdateIngredientColorCommand : CommandBase
    {
        public Guid Id { get; private set; }

        public string NewColor { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public UpdateIngredientColorCommand(Guid id, string newColor, Guid pieId)
        {
            Verify.NotWhitespace(newColor, "newColor");            
            Id = id;
            NewColor = newColor;
            PieId = pieId;
        }
    }
}