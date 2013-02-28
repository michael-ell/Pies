using System.Configuration;

namespace Codell.Pies.Data.Storage.Configuration
{
    public class ModuleSection : ConfigurationSection
    {
        [ConfigurationProperty(PropertyNames.ModuleTypeName, IsRequired = true)]
        public string TypeName
        {
            get { return (string)this[PropertyNames.ModuleTypeName]; }
            set { this[PropertyNames.ModuleTypeName] = value; }
        }

        public static class PropertyNames
        {
            public const string ModuleTypeName = "typeName";
        }         
    }
}