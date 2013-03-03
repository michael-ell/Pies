using Codell.Pies.Testing.FluentFixtures;
using NHibernate;
using Xunit;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.Infrastructure
{
    public class DatabaseContext : IUseFixture<DatabaseFixture>
    {
        protected ISessionFactory SessionFactory { get; private set; }

        public PersistFixtures.FixtureContext Using(ISession session)
        {
            return new PersistFixtures.FixtureContext(session);
        }

        public New New { get { return new New(new FixtureContext()); } }

        public void SetFixture(DatabaseFixture data)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            SessionFactory = data.SessionFactory;
        }
    }
}