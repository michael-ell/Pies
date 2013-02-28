using System.Configuration;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.Configuration
{
    public abstract class StorageConfigurationProviderBase
    {
        protected abstract string GroupName { get; }

        public ModuleSection ModuleConfiguration { get; private set; }

        public StorageSection Configuration { get; private set; }

        protected StorageConfigurationProviderBase()
        {
            GetModuleType();
            GetConfiguration();
        }

        protected void GetModuleType()
        {
            var name = string.Format("{0}/{1}", GroupName, StorageSectionGroup.PropertyNames.Module);
            ModuleConfiguration = GetConfiguration<ModuleSection>(name);
        }

        protected void GetConfiguration()
        {
            var name = string.Format("{0}/{1}", GroupName, StorageSectionGroup.PropertyNames.Store);
            Configuration = GetConfiguration<StorageSection>(name);
        }

        private TSection GetConfiguration<TSection>(string name) where TSection : ConfigurationSection
        {
            var configuration = ConfigurationManager.GetSection(name) as TSection;
            if (configuration == null)
                throw new ConfigurationErrorsException(string.Format(Resources.MissingConfiguration, name));
            return configuration;
        }
    }
}