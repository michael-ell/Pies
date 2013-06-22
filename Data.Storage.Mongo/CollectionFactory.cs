using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Data.Storage.Configuration;
using MongoDB.Driver;

namespace Codell.Pies.Data.Storage.Mongo
{
    public class CollectionFactory : ICollectionFactory
    {
        private readonly ICollectionNameMap _map;

        public MongoDatabase Database { get; private set; }

        public CollectionFactory(IApplicationStorageConfigurationProvider provider, 
                                 ISettings settings,
                                 ICollectionNameMap map)
        {
            Verify.NotNull(provider, "provider");
            Verify.NotNull(settings, "settings");            
            Verify.NotNull(map, "map");

            var url = new MongoUrl(settings.Get<string>(Keys.ConnectionStringPrefix) + provider.Configuration.GetConnectionString());
            var server = MongoServer.Create(url);
            Database = server.GetDatabase(url.DatabaseName);
            Database.SetProfilingLevel(provider.Configuration.Profile ? ProfilingLevel.All : ProfilingLevel.None);
            _map = map;
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(_map.GetNameFor<T>());
        }       
    }
}