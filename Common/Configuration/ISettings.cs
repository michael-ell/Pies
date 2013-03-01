namespace Codell.Pies.Common.Configuration
{
    public interface ISettings
    {
        T Get<T>(string key);
    }
}