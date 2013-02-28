using MongoDB.Driver;

namespace Codell.Pies.Data.Storage.Mongo.Schema
{
    public static class MongoDatabaseExtensions
    {
         public static void DropCollectionIfExists(this MongoDatabase database, string collectionName)
         {
             if (database == null) return;
             if (database.CollectionExists(collectionName))
             {
                 database.DropCollection(collectionName);
             }
         }
    }
}