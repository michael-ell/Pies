namespace Codell.Pies.Data.Storage.Configuration
{
    public class ApplicationStorageConfigurationProvider : StorageConfigurationProviderBase, IApplicationStorageConfigurationProvider
    {
        protected override string GroupName { get { return "Data.Storage.Application"; } }
    }
}