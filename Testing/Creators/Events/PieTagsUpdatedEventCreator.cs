using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Events
{
    public class PieTagsUpdatedEventCreator : Creator<PieTagsUpdatedEvent>
    {
        public PieTagsUpdatedEventCreator(IFixtureContext context) : base(context, new PieTagsUpdatedEvent(new List<Tag>{"a", "b", "c"}))
        {
        }

        public PieTagsUpdatedEventCreator With(string tag)
        {
            var tags = new HashSet<Tag>(Creation.NewTags) {tag};
            Creation = new PieTagsUpdatedEvent(tags);
            return this;
        }
    }
}