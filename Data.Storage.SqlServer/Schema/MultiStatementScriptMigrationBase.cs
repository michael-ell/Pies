using System;
using System.Data.SqlClient;
using System.Linq;
using FluentMigrator;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public abstract class MultiStatementScriptMigrationBase : Migration
    {
        protected abstract ISqlReader Reader { get; }

        protected SqlMigrationContext Context
        {
            get
            {
                var context = ApplicationContext as SqlMigrationContext;
                if (context == null) 
                    throw new ApplicationException(string.Format("Expected ApplicationContext to be of type {0}. Ensure the runner's context has been set.", typeof(SqlMigrationContext).Name));
                return context;
            }
        }

        public override void Up()
        {
            var scripts = Context.GetSqlScriptsUsing(Reader);
            using (var connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();
                foreach (var script in scripts)
                {
                    var statements = script.Split(new[] { "GO\r\n", "GO ", "GO\t", "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var command in statements.Select(statement => new SqlCommand(statement, connection)))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw new ApplicationException(string.Format("Failed to execute the statement '{0}'", command.CommandText), e);
                        }
                    }
                }
                connection.Close();
            } 
        }

        public override void Down()
        {            
        }
    }
}