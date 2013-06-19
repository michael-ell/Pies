namespace Codell.Pies.Core.Services
{
    public interface ICleaner
    {
        Cleaner.Result Clean(string value);
    }
}