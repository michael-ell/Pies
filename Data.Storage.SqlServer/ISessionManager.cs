using NHibernate;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public interface ISessionManager
    {
        ISession GetSession();
    }
}