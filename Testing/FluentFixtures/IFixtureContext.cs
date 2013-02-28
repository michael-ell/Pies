namespace Codell.Pies.Testing.FluentFixtures
{
    public interface IFixtureContext
    {
        Existing Existing { get; }
        Current Current { get; }
        New New { get; }
    }
}
