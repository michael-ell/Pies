using System;
using System.Linq;
using System.Reflection;
using Codell.Pies.Common;
using Codell.Pies.Common.Extensions;
using MongoDB.Driver;

namespace Codell.Pies.Data.Storage.Mongo.Schema
{
    public class Migrator : IMigrator
    {
        private readonly ICollectionFactory _factory;
        private readonly MongoDatabase _database;

        private MongoCollection<VersionInfo> Versions
        {
            get { return _factory.GetCollection<VersionInfo>(); }
        }

        public Migrator(ICollectionFactory factory)
        {
            Verify.NotNull(factory, "factory");
            _factory = factory;
            _database = _factory.Database;
        }

        public void Migrate()
        {
            var current = GetCurrentVersion();
            var types = Assembly.GetExecutingAssembly().GetTypesImplementing(typeof(IMigration));
            var migrations = types.Select(t => (IMigration)Activator.CreateInstance(t))
                                  .Where(migration => migration.Version > current)
                                  .OrderBy(migration => migration.Version)
                                  .ToList();
            migrations.ForEach(Apply);
        }

        private long GetCurrentVersion()
        {
            var current = Versions.FindAll().OrderBy(info => info.Version).LastOrDefault();
            return current == null ? 0 : current.Version;
        }

        private void Apply(IMigration migration)
        {
            migration.MigrateTo(_database);
            Versions.Save(new VersionInfo(migration.Version));
        }

        internal class VersionInfo
        {
            public long Version { get; private set; }

            public DateTime AppliedOn { get; private set; }

            public VersionInfo(long version)
            {
                Version = version;
                AppliedOn = DateTime.Now;
            }
        }
    }
}