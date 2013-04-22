using System;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.ReadModels
{
    public class TagCreator : Creator<Tag>
    {
        public TagCreator(IFixtureContext context) : base(context, new Tag())
        {
            Creation.Id = Guid.NewGuid();
            Creation.Value = Guid.NewGuid().ToString();
        }
    }
}