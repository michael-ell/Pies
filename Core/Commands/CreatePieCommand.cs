using System;
using Codell.Pies.Common;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootConstructor(typeof(Pie))]
    public class CreatePieCommand : CommandBase
    {
        public Guid Id { get; private set; }
        public IUser Owner { get; private set; }

        public CreatePieCommand(Guid id, IUser owner)
        {
            Verify.NotNull(owner, "owner");            
            Id = id;
            Owner = owner;
        }
    }
}