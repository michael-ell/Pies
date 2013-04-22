using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateTags")]
    public class UpdatePieTagsCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public IEnumerable<Tag> NewTags { get; private set; }

        public UpdatePieTagsCommand(Guid id, string newTags)
        {
            Id = id;
            NewTags = (newTags ?? "").Split(' ').Where(value => value.IsNotEmpty()).Select(value => new Tag(value)).ToList();
        } 

        public UpdatePieTagsCommand(Guid id, IEnumerable<string> newTags) : this(id, newTags.Safe().Select(value => new Tag(value)).ToList())
        {
        } 

        public UpdatePieTagsCommand(Guid id, IEnumerable<Tag> newTags)
        {
            NewTags = newTags ?? new List<Tag>();
            Id = id;
        }         
    }
}