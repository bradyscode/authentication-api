using authentication_class_library.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace authentication_class_library.Services
{
    public class ConnectionStringBuilder
    {

        private DatabaseOptions _dbOptions;

        public ConnectionStringBuilder(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }
        public DatabaseOptions Database { get; set; }
        public string ConnectionString { get; set; }

        public string BuildConnectionString()
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            // Pass the SqlConnectionStringBuilder an existing
            // connection string, and you can retrieve and
            // modify any of the elements.
            builder.ConnectionString = "server=(local);user id=ab;" +
                "password= a!Pass113;initial catalog=AdventureWorks";

            // Now that the connection string has been parsed,
            // you can work with individual items.
            builder.Password = _dbOptions.Password;
            builder.UserID = _dbOptions.UserId;
            builder.MultipleActiveResultSets = _dbOptions.MultipleActiveResultSets;
            builder["Database"] = _dbOptions.Database;
            builder["Server"] = _dbOptions.Server;
            return builder.ConnectionString;
        }

        public string BuildConnectionStringNoInitialCatalog()
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            // Pass the SqlConnectionStringBuilder an existing
            // connection string, and you can retrieve and
            // modify any of the elements.
            builder.ConnectionString = "server=(local);user id=ab;" +
                "password= a!Pass113;initial catalog=AdventureWorks";

            // Now that the connection string has been parsed,
            // you can work with individual items.
            builder.Password = _dbOptions.Password;
            builder.UserID = _dbOptions.UserId;
            builder.MultipleActiveResultSets = _dbOptions.MultipleActiveResultSets;
            builder["Database"] = null;
            builder["Server"] = _dbOptions.Server;
            return builder.ConnectionString;
        }
    }
}