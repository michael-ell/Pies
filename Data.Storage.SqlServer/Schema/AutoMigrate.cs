using System.Data.SqlClient;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public static class AutoMigrate
    {
        public static void Process(string connectionString)
        {
            if (connectionString.IsNotEmpty())
            {
                var databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

                SqlServerMigrator.From(typeof(AutoMigrate).Assembly)
                                 .With(new SqlMigrationContext(connectionString,
                                     s => s.Replace("WJ_TRAVEL_DEV", databaseName).Replace("wj_travel_dev", databaseName)))
                                 .Migrate();
            }
        }
    }
}
