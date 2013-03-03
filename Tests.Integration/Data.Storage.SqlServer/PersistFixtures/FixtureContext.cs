using NHibernate;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.PersistFixtures
{
    public class FixtureContext : IFixtureContext
    {
        private readonly Current _current;
        private readonly Existing _existing;
        private readonly ISession _session;

        public FixtureContext(ISession session)
        {
            _existing = new Existing(this);
            _current = new Current();
            _session = session;
        }

        public Existing Existing
        {
            get { return _existing; }
        }

        public Current Current
        {
            get { return _current; }
        }

        public IFixtureContext Create
        {
            get { return this; }
        }

        public IFixtureContext Get
        {
            get { return this; }
        }

        public ISession Session
        {
            get { return _session; }
        }
    }
}