namespace Codell.Pies.Data.Storage.Configuration
{
    public interface IApplicationStorageConfigurationProvider
    {
        ModuleSection ModuleConfiguration { get; }
        StorageSection Configuration { get; }
    }
}