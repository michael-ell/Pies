using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.Infrastructure
{
    public class Database : IDisposable
    {
        private string ManageDbConnectionString { get; set; }

        public string ManageSchemaConnectionString { get; private set; }

        public string TestUserConnectionString { get; private set; }

        public string Name { get; private set; }

        public Func<string, string> ScriptDbNameReplacer { get; private set; }

        public Database()
        {
            Name = Guid.NewGuid().ToString();
            SetConnectionStrings();
            ScriptDbNameReplacer = sql => sql.Replace("WJ_TRAVEL_DEV", Name).Replace("WJ_TRAVEL", Name);
            Create();
        }

        private void SetConnectionStrings()
        {
            const string catalog = "[catalog]";
            ManageDbConnectionString = ConfigurationManager.ConnectionStrings["DbManager"].ConnectionString.Replace(catalog, "master");
            ManageSchemaConnectionString = ConfigurationManager.ConnectionStrings["DbManager"].ConnectionString.Replace(catalog, Name);
            TestUserConnectionString = ConfigurationManager.ConnectionStrings["TestUser"].ConnectionString.Replace(catalog, Name);
        }

        private void Create()
        {
            using (var connection = new SqlConnection(ManageDbConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(string.Format("create database [{0}]", Name), connection);
                command.ExecuteNonQuery();
                connection.Close();
            } 
        }

        public void Dispose()
        {
            using (var connection = new SqlConnection(ManageDbConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(string.Format("alter database [{0}] set offline with rollback immediate", Name), connection);
                command.ExecuteNonQuery();
                command = new SqlCommand(string.Format("alter database [{0}] set online ", Name), connection);
                command.ExecuteNonQuery();
                command = new SqlCommand(string.Format("drop database [{0}]", Name), connection);
                command.ExecuteNonQuery();
                connection.Close();
            } 
        }
    }
}