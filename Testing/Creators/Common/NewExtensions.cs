using Codell.Pies.Common.Security;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Common
{
    public static class NewExtensions
    {
        public static NewCommon Common(this New @new)
        {
            return new NewCommon(@new);
        }

        public class NewCommon
        {
            public New New { get; private set; }

            public NewCommon(New @new)
            {
                New = @new;
            }

            public IUser User()
            {
                return new UserCreator(New.Context).Creation;
            }
        }
    }
}