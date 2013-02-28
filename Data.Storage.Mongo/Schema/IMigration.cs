using MongoDB.Driver;

namespace Codell.Pies.Data.Storage.Mongo.Schema
{
    public interface IMigration
    {
        long Version { get; }
        void MigrateTo(MongoDatabase database);
    }
}