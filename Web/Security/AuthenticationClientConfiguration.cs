using System.Configuration;
using Codell.Pies.Common;

namespace Codell.Pies.Web.Security
{
    public class AuthenticationClientConfiguration
    {
        public AuthenticationClientSection Microsoft { get; private set; }
        public AuthenticationClientSection Twitter { get; private set; }
        private const string GroupName = "authentication.client";

        public AuthenticationClientConfiguration()
        {
            GetMicrosoftConfiguration();
            GetTwitterConfiguration();
        }

        protected void GetMicrosoftConfiguration()
        {
            var name = string.Format("{0}/{1}", GroupName, AuthenticationClientSectionGroup.PropertyNames.Microsoft);
            Microsoft = GetConfiguration<AuthenticationClientSection>(name);
        }

        protected void GetTwitterConfiguration()
        {
            var name = string.Format("{0}/{1}", GroupName, AuthenticationClientSectionGroup.PropertyNames.Twitter);
            Twitter = GetConfiguration<AuthenticationClientSection>(name);
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