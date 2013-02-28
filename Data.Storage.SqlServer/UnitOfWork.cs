using Codell.Pies.Common;
using Codell.Pies.Core.Repositories;
using Common.Logging;
using NHibernate;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly ISession _session;

        public UnitOfWork(ISession session)
        {
            Verify.NotNull(session, "session");            
            _session = session;
        }

        public void Begin()
        {
            Log.Debug("Starting session transaction.");
            _session.BeginTransaction();
        }

        public void Commit()
        {
            var transaction = _session.Transaction;
            if (transaction.IsActive)
            {
                Log.Debug("Committing session changes.");
                transaction.Commit();
            }
            else
            {
                Log.Warn("Transaction not active.");
            }
        }

        public void Rollback()
        {
            var transaction = _session.Transaction;
            if (transaction.IsActive)
            {
                Log.Debug("Rolling back session changes.");
                transaction.Rollback();
            }
            else
            {
                Log.Warn("Transaction not active.");                
            }
        }
    }
}