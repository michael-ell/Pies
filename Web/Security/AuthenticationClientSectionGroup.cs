using System.Configuration;

namespace Codell.Pies.Web.Security
{
    public class AuthenticationClientSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty(PropertyNames.Microsoft, IsRequired = false)]
        public AuthenticationClientSection Microsoft
        {
            get { return (AuthenticationClientSection) Sections[PropertyNames.Microsoft]; }
        }

        [ConfigurationProperty(PropertyNames.Twitter, IsRequired = false)]
        public AuthenticationClientSection Twitter
        {
            get { return (AuthenticationClientSection)Sections[PropertyNames.Twitter]; }
        }

        public static class PropertyNames
        {
            public const string Microsoft = "microsoft";
            public const string Twitter = "twitter";
        }
    }
}