using System.Configuration;
using Codell.Pies.Common;
using Codell.Pies.Data.Storage.Configuration;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly IApplicationStorageConfigurationProvider _provider;

        public Bootstrapper(IApplicationStorageConfigurationProvider provider)
        {
            Verify.NotNull(provider, "provider");            
            _provider = provider;
        }

        public void Run()
        {
            if (_provider.Configuration.AutoMigrate)
            {
                var setting = ConfigurationManager.ConnectionStrings["WJ_TRAVEL_AUTOUPDATE"];
                if (setting != null)
                    AutoMigrate.Process(setting.ConnectionString);
            }
        }
    }
}