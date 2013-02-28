namespace Codell.Pies.Data.Storage.Configuration
{
    public class AuditStorageConfigurationProvider : StorageConfigurationProviderBase, IAuditStorageConfigurationProvider
    {
        protected override string GroupName { get { return "Data.Storage.Audit"; } }
    }
}