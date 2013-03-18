using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "AddIngredient")]
    public class AddIngredientCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public AddIngredientCommand(Guid id, string description)
        {
            Verify.NotWhitespace(description, "description");
            
            Id = id;
            Description = description;
        }
    }
}