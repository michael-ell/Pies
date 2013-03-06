using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class PieCreator : Creator<Pie>
    {
        public Guid Id { get; private set; }
        public PieCreator(IFixtureContext context) : base(context, null)
        {
            Id = Guid.NewGuid();
            Creation = new Pie(Id, Guid.NewGuid().ToString());
        }
    }
}