using MongoDB.Driver;

namespace Codell.Pies.Data.Storage.Mongo
{
    public interface ICollectionFactory
    {
        MongoCollection<T> GetCollection<T>();
        MongoDatabase Database { get; }
    }
}