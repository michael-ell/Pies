using System.IO;
using System.Reflection;
using System.Text;
using Codell.Pies.Common;
using Common.Logging;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class SqlServerMigrator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Assembly _assembly;
        private SqlMigrationContext _context;

        public static SqlServerMigrator From(Assembly assembly)
        {
            return new SqlServerMigrator(assembly);            
        }

        private SqlServerMigrator(Assembly assembly)
        {
            Verify.NotNull(assembly, "assembly");
            _assembly = assembly;
        }

        public SqlServerMigrator With(SqlMigrationContext context)
        {
            _context = context;
            return this;
        }

        public void Migrate()
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            using (var announcer = new TextWriterAnnouncer(writer))
            {
                var processorFactory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
                var processor = processorFactory.Create(_context.ConnectionString, announcer, new ProcessorOptions());
                var assembly = _assembly;
                var runner = new MigrationRunner(assembly, new RunnerContext(announcer) { ApplicationContext = _context }, processor);
                runner.MigrateUp();
                Log.Info(sb);
            }  
        }
    }
}