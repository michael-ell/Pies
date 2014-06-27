using System;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Services;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "AddIngredient")]
    public class AddIngredientCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; set; }

        public string Description { get; private set; }

        public ICleaner Cleaner { get; private set; }

        public ISettings Settings { get; private set; }

        public AddIngredientCommand(Guid id, string description)
        {
            //Verify.NotWhitespace(description, "description");
            
            Id = id;
            Description = (description ?? "").Trim();
            Cleaner = ServiceLocator.Instance.Find<ICleaner>();
            Settings = ServiceLocator.Instance.Find<ISettings>();
        }
    }
}