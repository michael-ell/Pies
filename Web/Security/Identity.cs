using Codell.Pies.Common;
using Codell.Pies.Common.Security;

namespace Codell.Pies.Web.Security
{
    public class Identity : IPiesIdentity
    {
        private readonly IUser _user;

        public Identity(IUser user)
        {
            Verify.NotNull(user, "user");            
            _user = user;
        }

        public string Name { get { return _user.Name; } }

        public string AuthenticationType { get { return "External Provider"; } }

        public bool IsAuthenticated { get { return true; } }

        public IUser User { get { return _user; } }
    }
}