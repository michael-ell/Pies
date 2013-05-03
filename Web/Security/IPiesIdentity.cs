using System.Security.Principal;

namespace Codell.Pies.Web.Security
{
    public interface IPiesIdentity : IIdentity
    {
        OpenIdUser User { get; }
    }
}