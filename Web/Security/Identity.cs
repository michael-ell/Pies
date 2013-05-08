using Codell.Pies.Common;
using Codell.Pies.Common.Security;

namespace Codell.Pies.Web.Security
{
    public class Identity : IPiesIdentity
    {
        private readonly OpenIdUser _user;

        public Identity(OpenIdUser user)
        {
            Verify.NotNull(user, "user");            
            _user = user;
        }

        public string Name
        {
            get 
            { 
                return _user.Nickname.IsNotEmpty() ? _user.Nickname 
                                                   : (_user.FullName.IsNotEmpty() ? _user.FullName : _user.Email); 
            }
        }

        public string AuthenticationType
        {
            get { return "Open Id"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public IUser User
        {
            get { return _user; }
        }
    }
}