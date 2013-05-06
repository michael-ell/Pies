namespace Codell.Pies.Web.Security
{
    public class AnonymousIdentity : IPiesIdentity
    {
        public string Name { get { return string.Empty; } }
        public string AuthenticationType { get { return string.Empty; } }
        public bool IsAuthenticated { get { return false; } }
        public OpenIdUser User { get { return OpenIdUser.Empty; } }
    }
}