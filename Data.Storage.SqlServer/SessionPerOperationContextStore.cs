using System.Collections;
using System.ServiceModel;
using System.Threading;
using Codell.Pies.Common;
using NHibernate;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public class SessionPerOperationContextStore : ISessionStore
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Hashtable _store;

        public SessionPerOperationContextStore(ISessionFactory sessionFactory)
        {
            Verify.NotNull(sessionFactory, "sessionFactory");            
            _sessionFactory = sessionFactory;
            _store = Hashtable.Synchronized(new Hashtable());
        }

        private bool HasActiveSession
        {
            get { return _store.Contains(GetSessionKey()); }
        }

        public ISession GetActiveSession()
        {
            if (HasActiveSession)
            {
                return _store[GetSessionKey()] as ISession;        
            }

            var session = _sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            _store.Add(GetSessionKey(), session);
            OperationContext.Current.OperationCompleted += (sender, args) => RemoveActiveSession();
            return session;
        }

        public void RemoveActiveSession()
        {
            if (HasActiveSession)
            {
                _store.Remove(GetSessionKey());
            }
        }

        private string GetSessionKey()
        {
            return string.Format("{0}_{1}", OperationContext.Current.SessionId, Thread.CurrentContext.ContextID);
        }
    }
}