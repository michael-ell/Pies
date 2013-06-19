namespace Codell.Pies.Core.Services
{
    public class DoNothingCleaner : ICleaner
    {
        public Cleaner.Result Clean(string value)
        {
            return new Cleaner.Result(false, value);
        }
    }
}