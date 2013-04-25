using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateIngredientDescription")]
    public class UpdateIngredientDescriptionCommand : CommandBase
    {
        public Guid Id { get; private set; }

        public string NewDescription { get; private set; }

        [AggregateRootId]
        public Guid PieId { get; private set; }

        public UpdateIngredientDescriptionCommand(Guid id, string newDescription, Guid pieId)
        {
            Verify.NotWhitespace(newDescription, "newDescription");            
            Id = id;
            NewDescription = newDescription;
            PieId = pieId;
        }
    }
}