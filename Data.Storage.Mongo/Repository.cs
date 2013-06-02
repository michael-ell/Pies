using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codell.Pies.Common;
using Codell.Pies.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Codell.Pies.Data.Storage.Mongo
{
    public class Repository : IRepository
    {
        private readonly ICollectionFactory _factory;

        public Repository(ICollectionFactory factory)
        {
            Verify.NotNull(factory, "factory");

            _factory = factory;
        }

        public void Save<TEntity>(TEntity toSave)
        {
            CollectionFor<TEntity>().Save(toSave);
        }

        public long Count<TEntity>()
        {
            return CollectionFor<TEntity>().Count();
        }

        public TEntity FindById<TId, TEntity>(TId id)
        {
            return CollectionFor<TEntity>().FindOneById(BsonValue.Create(id));
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return CollectionFor<TEntity>().FindAll().ToList();
        }

        public IQueryable<TEntity> Get<TEntity>()
        {
            return CollectionFor<TEntity>().AsQueryable<TEntity>();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {            
            return CollectionFor<TEntity>().AsQueryable<TEntity>().Where(predicate).ToList();
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Func<TEntity, TProjected> projection)
        {
            return CollectionFor<TEntity>().AsQueryable().Select(projection).ToList();
        }

        public IEnumerable<TProjected> Project<TEntity, TProjected>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TProjected> projection)
        {
            return CollectionFor<TEntity>().AsQueryable().Where(predicate).ToList().Select(projection);
        }

        public void DeleteById<TId, TEntity>(TId id)
        {
            CollectionFor<TEntity>().Remove(Query.EQ("_id", BsonValue.Create(id)));
        }

        private MongoCollection<TEntity> CollectionFor<TEntity>()
        {
            return _factory.GetCollection<TEntity>();
        }
    }
}