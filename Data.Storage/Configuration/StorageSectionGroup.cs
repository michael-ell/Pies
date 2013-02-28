using System.Configuration;

namespace Codell.Pies.Data.Storage.Configuration
{
    public class StorageSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty(PropertyNames.Store, IsRequired = true)]
        public StorageSection Store
        {
            get { return (StorageSection)Sections[PropertyNames.Store]; }
        }

        public static class PropertyNames
        {
            public const string Module = "module";
            public const string Store = "store";
        }
    }
}