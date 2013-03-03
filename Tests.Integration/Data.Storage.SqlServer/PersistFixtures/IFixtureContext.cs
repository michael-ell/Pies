using NHibernate;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.PersistFixtures
{
    public interface IFixtureContext
    {
        Existing Existing { get; }

        Current Current { get; }

        IFixtureContext Create { get; }

        ISession Session { get; }
    }
}