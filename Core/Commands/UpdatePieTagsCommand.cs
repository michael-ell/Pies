using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateTags")]
    public class UpdatePieTagsCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public IEnumerable<string> NewTags { get; private set; }

        public UpdatePieTagsCommand(Guid id, string newTags) : this(id, (newTags ?? "").Split(' ').ToList())
        {
        } 

        public UpdatePieTagsCommand(Guid id, IEnumerable<string> newTags)
        {
            Id = id;
            NewTags = newTags;
        }  
    }
}