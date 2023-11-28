using authentication_dot_net.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace authentication_dot_net.Middleware
{
    public class DatabaseMiddleware
    {
        private IOptions<DatabaseOptions> _dbOptions;

        public DatabaseMiddleware(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions;
        }


        public void CheckDatabaseExistsAndCreateDatabase()
        {
            var csb = new ConnectionStringBuilder(_dbOptions);
            var connectionString = csb.BuildConnectionStringNoInitialCatalog();
            var sqlScriptPath = "create-database-table.sql";

            using (var connection = new SqlConnection(connectionString))
            {
                var databaseName = _dbOptions.Value.Database;
                var tableName = "Users";

                // Check if the database exists
                if(!DatabaseExists(connectionString, databaseName))
                {
                    connection.Execute($"CREATE DATABASE {_dbOptions.Value.Database}");
                }

                // Check if the table exists

                if (!TableExists(connectionString,tableName))
                {
                    var sqlScriptContent = File.ReadAllText(sqlScriptPath);
                    connection.Execute(sqlScriptContent);
                }
            }
        }

        private bool DatabaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var result = connection.QuerySingleOrDefault<bool>($"SELECT 1 FROM sys.databases WHERE name = @databaseName", new { databaseName });
                return result != null;
            }
        }
        private bool TableExists(string connectionString, string tableName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var result = connection.QuerySingleOrDefault<bool>($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName", new { tableName });
                return result;
            }
        }
    }
}
