using System;
using System.Configuration;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.Configuration
{
    public class StorageSection : ConfigurationSection
    {        
        [ConfigurationProperty(PropertyNames.ConnectionStringName, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string)this[PropertyNames.ConnectionStringName]; }
            set { this[PropertyNames.ConnectionStringName] = value; }
        }

        [ConfigurationProperty(PropertyNames.Profile, IsRequired = false)]
        public bool Profile
        {
            get { return (bool)this[PropertyNames.Profile]; }
            set { this[PropertyNames.Profile] = value; }
        }

        [ConfigurationProperty(PropertyNames.AutoMigrate, IsRequired = false)]
        public bool AutoMigrate
        {
            get { return (bool)this[PropertyNames.AutoMigrate]; }
            set { this[PropertyNames.AutoMigrate] = value; }
        }


        public string GetConnectionString()
        {
            var value = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (value == null)
            {
                throw new ApplicationException(string.Format(Resources.MissingConnectionString, ConnectionStringName));
            }
            return value.ConnectionString;
        }

        public static class PropertyNames
        {
            public const string ConnectionStringName = "connectionStringName";
            public const string Profile = "profile";
            public const string AutoMigrate = "autoMigrate";
        }
    }
}