using System.Security.Principal;
using Codell.Pies.Common.Security;

namespace Codell.Pies.Web.Security
{
    public interface IPiesIdentity : IIdentity
    {
        IUser User { get; }
    }
}