using System;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.FluentFixtures;
using Moq;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class PieCreator : Creator<Pie>
    {
        public Guid Id { get; private set; }
        public Mock<ISettings> MockSettings { get; private set; }
        public ISettings Settings { get { return MockSettings.Object; } }

        public PieCreator(IFixtureContext context) : base(context, null)
        {
            Id = Guid.NewGuid();
            var pie = new Pie(Id); 
            Creation = pie;
            MockSettings = new Mock<ISettings>();
            MockSettings.Setup(settings => settings.Get<int>(Keys.MaxIngredients)).Returns(100);
        }       

        public PieCreator AddIngredient(string description)
        {
            Creation.AddIngredient(description, MockSettings.Object);
            return this;
        }
    }
}