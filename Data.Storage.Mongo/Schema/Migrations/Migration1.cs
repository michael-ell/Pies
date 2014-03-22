using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Codell.Pies.Data.Storage.Mongo.Schema.Migrations
{
    public class Migration1 : IMigration
    {
        public long Version { get { return 1; } }

        public void MigrateTo(MongoDatabase database)
        {
            database.GetCollection("Pies").Update(Query.Null, Update.Set("IsPrivate", BsonBoolean.Create(false)), UpdateFlags.Multi);
        }
    }
}