using System.Security.Principal;

namespace Codell.Pies.Common.Security
{
    public interface IPiesIdentity : IIdentity
    {
        IUser User { get; }
    }
}