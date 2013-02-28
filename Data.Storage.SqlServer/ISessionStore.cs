using NHibernate;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public interface ISessionStore
    {
        ISession GetActiveSession();

        void RemoveActiveSession();
    }
}