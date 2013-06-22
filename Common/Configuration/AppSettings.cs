using System;
using System.Configuration;

namespace Codell.Pies.Common.Configuration
{
    public class AppSettings : ISettings
    {
        public T Get<T>(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (value == null) throw new ApplicationException(string.Format(Resources.MissingAppSetting, key));
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}