using System;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Services;
using Codell.Pies.Testing.FluentFixtures;
using Moq;
using Codell.Pies.Testing.Creators.Common;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class PieCreator : Creator<Pie>
    {
        public Guid Id { get; private set; }
        public Mock<ICleaner> MockCleaner { get; private set; }
        public ICleaner Cleaner { get { return MockCleaner.Object; } }
        public Mock<ISettings> MockSettings { get; private set; }
        public ISettings Settings { get { return MockSettings.Object; } }
        public IUser Owner { get; private set; }

        public PieCreator(IFixtureContext context) : base(context, null)
        {
            Id = Guid.NewGuid();
            Owner = New.Common().User();
            var pie = new Pie(Id, Owner); 
            Creation = pie;
            MockCleaner = new Mock<ICleaner>();
            MockSettings = new Mock<ISettings>();
            MockSettings.Setup(settings => settings.Get<int>(Keys.MaxIngredients)).Returns(100);
        }       

        public PieCreator AddIngredient(string description)
        {
            MockCleaner.Setup(cleaner => cleaner.Clean(description)).Returns(new Cleaner.Result(false, description));
            Creation.AddIngredient(description, MockCleaner.Object, MockSettings.Object);
            return this;
        }
    }
}