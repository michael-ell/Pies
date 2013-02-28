using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class SqlMigrationContext
    {
        private readonly Func<string, string> _tweaker;

        public SqlMigrationContext(string connectionString) : this(connectionString, null)
        {

        }

        public SqlMigrationContext(string connectionString, Func<string, string> sqlTweaker)
        {
            Verify.NotWhitespace(connectionString, "connectionString");

            ConnectionString = connectionString;
            _tweaker = sqlTweaker ?? (sql => sql);
        }

        public string ConnectionString { get; private set; }

        public IEnumerable<string> GetSqlScriptsUsing(ISqlReader reader)
        {
            Verify.NotNull(reader, "reader");
            return reader.ReadSqlScripts().Select(sql => _tweaker.Invoke(sql));
        }
    }
}