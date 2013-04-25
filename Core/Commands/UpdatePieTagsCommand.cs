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

        public UpdatePieTagsCommand(Guid id, string newTags)
        {
            var splitter = new Splitter(' ', new Splitter(',', new Splitter(';', null)));
            Id = id;
            NewTags = splitter.Split(newTags ?? "").ToList();
        } 

        public UpdatePieTagsCommand(Guid id, IEnumerable<string> newTags)
        {
            Id = id;
            NewTags = newTags;
        }  

        private class Splitter
        {
            private readonly char _separator;
            private readonly Splitter _successor;

            public Splitter(char separator, Splitter successor)
            {
                _separator = separator;
                _successor = successor;
            }

            public IEnumerable<string> Split(string val)
            {
                var tags = val.Split(_separator);
                if (tags.Length == 1 && _successor != null)
                {
                    return _successor.Split(val);
                }
                return tags;
            }
        }
    }
}