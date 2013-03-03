using NHibernate;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.PersistFixtures
{
    public class FixtureContextAware
    {
        private IFixtureContext _context;

        protected FixtureContextAware(IFixtureContext context)
        {
            _context = context;
        }

        protected FixtureContextAware()
        {
        }

        protected Current Current
        {
            get { return _context.Current; }
        }

        protected Existing Existing
        {
            get { return _context.Existing; }
        }

        protected ISession Session
        {
            get { return _context.Session; }
        }

        public IFixtureContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
    }
}