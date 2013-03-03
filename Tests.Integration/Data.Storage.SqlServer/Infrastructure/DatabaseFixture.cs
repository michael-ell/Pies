using System;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Data.Storage.SqlServer.Schema;
using NHibernate;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.Infrastructure
{
    public class DatabaseFixture : IDisposable
    {
        private Database Database { get; set; }

        public ISessionFactory SessionFactory { get; private set; }

        public DatabaseFixture()
        {        
            Database = new Database();
            Migrate();
            SessionFactory = BuildSessionFactory();
        }

        private void Migrate()
        {
            var context = new SqlMigrationContext(Database.ManageSchemaConnectionString, Database.ScriptDbNameReplacer);
            SqlServerMigrator.From(typeof(AutoMigrate).Assembly).With(context).Migrate();
        }

        private ISessionFactory BuildSessionFactory()
        {
            return Pies.Data.Storage.SqlServer.SessionFactory.Build(Database.TestUserConnectionString, typeof(Pie).Assembly) ;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}