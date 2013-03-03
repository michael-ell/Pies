using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Helpers
{
    public static class NewExtensions
    {
        public static ListCreator<T> List<T>(this New newInstance) where T : new()
        {
            return new ListCreator<T>(newInstance.Context);
        }
    }
}