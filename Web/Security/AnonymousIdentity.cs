using Codell.Pies.Common.Security;

namespace Codell.Pies.Web.Security
{
    public class AnonymousIdentity : IPiesIdentity
    {
        public string Name { get { return string.Empty; } }
        public string AuthenticationType { get { return string.Empty; } }
        public bool IsAuthenticated { get { return false; } }
        public IUser User { get { return OpenIdUser.Anonymous; } }
    }
}