using Codell.Pies.Common.Security;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Common
{
    public class UserCreator : Creator<IUser>
    {
        public UserCreator(IFixtureContext context) : base(context, null)
        {
            Creation = new StubUser
                           {
                               Email = "abc@xyz.com",
                               FullName = "Bob Marley",
                               Nickname = "Rasta-man"
                           };
        }

        private class StubUser : IUser
        {
            public string Email { get; set; }
            public string Nickname { get; set; }
            public string FullName { get; set; }
        }
    }
}