using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codell.Pies.Common;
using Codell.Pies.Core.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public class Repository : IRepository
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            Verify.NotNull(session, "session");

            _session = session;
        }

        public void Save<TEntity>(TEntity toSave)
        {
            _session.SaveOrUpdate(toSave);
        }

        public TEntity FindById<TId, TEntity>(TId id)
        {
            return _session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return _session.Query<TEntity>().Where(predicate);
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Func<TEntity, TProjected> projection)
        {
            return _session.Query<TEntity>().Select(projection);
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TProjected> projection)
        {
            return _session.Query<TEntity>().Where(predicate).Select(projection);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity: class
        {
            return _session.QueryOver<TEntity>().List();
        }

        public void DeleteById<TId, TEntity>(TId id)
        {
            var entity = FindById<TId, TEntity>(id);
            _session.Delete(entity);
        }
    }
}