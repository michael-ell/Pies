﻿using System;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.FluentFixtures;
using Moq;
using Codell.Pies.Testing.Creators.Common;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class PieCreator : Creator<Pie>
    {
        public Guid Id { get; private set; }
        public Mock<ISettings> MockSettings { get; private set; }
        public ISettings Settings { get { return MockSettings.Object; } }
        public IUser Owner { get; private set; }

        public PieCreator(IFixtureContext context) : base(context, null)
        {
            Id = Guid.NewGuid();
            Owner = New.Common().User();
            var pie = new Pie(Id, Owner); 
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