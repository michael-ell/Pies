using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Codell.Pies.Core.Repositories
{
    public interface IRepository
    {
        void Save<TEntity>(TEntity toSave);
        long Count<TEntity>();
        TEntity FindById<TId, TEntity>(TId id);
        IQueryable<TEntity> Get<TEntity>();
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        void DeleteById<TId, TEntity>(TId id);
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TProjected> Project<TEntity, TProjected>(Func<TEntity, TProjected> projection);
        IEnumerable<TProjected> Project<TEntity, TProjected>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TProjected> projection);
    }
}