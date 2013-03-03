using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "Slice")]
    public class SlicePieCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public int Percent { get; private set; }

        public string Description { get; private set; }

        public SlicePieCommand(Guid id, int percent, string description)
        {
            Verify.NotWhitespace(description, "description");            

            Id = id;
            Percent = percent;
            Description = description;
        }
    }
}