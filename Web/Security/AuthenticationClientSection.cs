using System.Configuration;

namespace Codell.Pies.Web.Security
{
    public class AuthenticationClientSection : ConfigurationSection
    {
        [ConfigurationProperty(PropertyNames.Key, IsRequired = true)]
        public string Key
        {
            get { return (string)this[PropertyNames.Key]; }
            set { this[PropertyNames.Key] = value; }
        }

        [ConfigurationProperty(PropertyNames.Secret, IsRequired = true)]
        public string Secret
        {
            get { return (string)this[PropertyNames.Secret]; }
            set { this[PropertyNames.Secret] = value; }
        } 
        
        private static class PropertyNames
        {
            public const string Key = "key";
            public const string Secret = "secret";

        }
    }
}