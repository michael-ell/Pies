namespace Codell.Pies.Testing.FluentFixtures
{
	public class FixtureContext : IFixtureContext
	{
        private readonly New _new;
		private readonly Existing _existing;
		private readonly Current _current;

		public Existing Existing
		{
			get { return _existing; }
		}

		public Current Current
		{
			get { return _current; }
		}

		public New New
		{
			get { return _new; }
		}

		public FixtureContext()
		{
            _new = new New(this);
			_existing = new Existing(this);
			_current = new Current();
		}
	}
}
