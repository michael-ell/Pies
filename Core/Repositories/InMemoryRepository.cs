using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Repositories
{
    public class InMemoryRepository : IRepository
    {
        private readonly ConcurrentDictionary<Type, object> _stores;

        public InMemoryRepository()
        {
            _stores = new ConcurrentDictionary<Type, object>();
        }

        public void Save<TEntity>(TEntity toSave)
        {
            var store = GetStoreFor<TEntity>();
            if (!store.Contains(toSave))
                store.Add(toSave);
        }

        public long Count<T>()
        {
            return GetStoreFor<T>().Count;
        }

        public T FindById<TId, T>(TId id)
        {
            var predicate = new PredicateFactory<T>();
            return GetStoreFor<T>().SingleOrDefault(predicate.Create("Id", id));
        }

        public IQueryable<TEntity> Get<TEntity>()
        {
            return GetStoreFor<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return GetStoreFor<TEntity>().Where(predicate.Compile());
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Func<TEntity, TProjected> projection)
        {
            return GetStoreFor<TEntity>().Select(projection);
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TProjected> projection)
        {
            return GetStoreFor<TEntity>().Where(predicate.Compile()).Select(projection);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return GetStoreFor<T>();
        }

        public void DeleteById<TId, T>(TId id)
        {
            var store = GetStoreFor<T>();            
            var item = FindById<TId, T>(id);
            if (!EqualityComparer<T>.Default.Equals(item, default(T)))
            {
                store.Remove(item);
            }
        }

        private SynchronizedCollection<T> GetStoreFor<T>()
        {
            object store;
            if (!_stores.TryGetValue(typeof(T), out store))
            {
                store = new SynchronizedCollection<T>();
                _stores[typeof (T)] = store;
            }
            return (SynchronizedCollection<T>)store;
        }

        private class PredicateFactory<T>
        {
            public Func<T, bool> Create(string suffix, object value)
            {
                var pe = Expression.Parameter(typeof(T), "entity");
                var propertyName = ResolvePropertyName(suffix);
                var predicate = FalsePredicate(pe);
                if (propertyName.IsNotEmpty())
                    predicate = CreateEqualsPredicateFrom(propertyName, value, pe);
                return predicate.Compile();
            }

            private string ResolvePropertyName(string suffix)
            {
                var pi = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                  .FirstOrDefault(property => property.Name.EndsWith(suffix));
                return pi == null ? null : pi.Name;
            }

            private Expression<Func<T, bool>> FalsePredicate(ParameterExpression pe)
            {
                return Expression.Lambda<Func<T, bool>>(Expression.Constant(false), new[] { pe });
            }

            private Expression<Func<T, bool>> CreateEqualsPredicateFrom(string propertyName, object value, ParameterExpression pe)
            {
                var propertyValue = Expression.Property(pe, propertyName);
                Expression filterValue = Expression.Constant(value);
                var predicate = Expression.Equal(propertyValue, filterValue);
                return Expression.Lambda<Func<T, bool>>(predicate, new[] { pe });
            }
        }
    }
}