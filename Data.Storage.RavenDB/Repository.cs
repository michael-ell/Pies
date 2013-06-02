using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codell.Pies.Core.Repositories;

namespace Codell.Pies.Data.Storage.RavenDB
{
    public class Repository : IRepository
    {
        public void Save<TEntity>(TEntity toSave)
        {
            throw new NotImplementedException();
        }

        public long Count<TEntity>()
        {
            throw new NotImplementedException();
        }

        public TEntity FindById<TId, TEntity>(TId id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get<TEntity>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void DeleteById<TId, TEntity>(TId id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Func<TEntity, TProjected> projection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TProjected> projection)
        {
            throw new NotImplementedException();
        }
    }
}