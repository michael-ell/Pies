using System;
using Codell.Pies.Common.Security;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Common
{
    public class UserCreator : Creator<IUser>
    {
        public UserCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new StubUser(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        private class StubUser : IUser
        {
            public StubUser(string id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Id { get; private set; }

            public string Name { get; private set; }
        }
    }

}