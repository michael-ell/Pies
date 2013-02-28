using Codell.Pies.Common;
using Codell.Pies.Data.Storage.Configuration;

namespace Codell.Pies.Data.Storage.Mongo.Schema
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly IApplicationStorageConfigurationProvider _provider;
        private readonly IMigrator _migrator;

        public Bootstrapper(IApplicationStorageConfigurationProvider provider, IMigrator migrator)
        {
            Verify.NotNull(provider, "provider");            
            Verify.NotNull(migrator, "migrator");
            _provider = provider;
            _migrator = migrator;
        }

        public void Run()
        {
            if (_provider.Configuration.AutoMigrate)
            {
                _migrator.Migrate();
            }
        }
    }
}