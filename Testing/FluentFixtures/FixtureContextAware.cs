namespace Codell.Pies.Testing.FluentFixtures
{
    public class FixtureContextAware
    {
        private IFixtureContext _context;

        protected New New
        {
            get { return _context.New; }
        }

        protected Current Current
        {
            get { return _context.Current; }
        }

        protected Existing Existing
        {
            get { return _context.Existing; }
        }

        public IFixtureContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        protected FixtureContextAware(IFixtureContext context)
        {
            _context = context;
        }

        protected FixtureContextAware()
        {
        }
    }
}
