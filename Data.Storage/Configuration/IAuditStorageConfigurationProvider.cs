namespace Codell.Pies.Data.Storage.Configuration
{
    public interface IAuditStorageConfigurationProvider
    {
        ModuleSection ModuleConfiguration { get; }
        StorageSection Configuration { get; }
    }
}