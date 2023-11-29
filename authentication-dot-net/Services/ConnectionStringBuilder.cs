using authentication_dot_net.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace authentication_dot_net.Services
{
    public class ConnectionStringBuilder
    {

        private IOptions<DatabaseOptions> _dbOptions;

        public ConnectionStringBuilder(IOptions<DatabaseOptions> dbOptions)
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
            builder.Password = _dbOptions.Value.Password;
            builder.UserID = _dbOptions.Value.UserId;
            builder.MultipleActiveResultSets = _dbOptions.Value.MultipleActiveResultSets;
            builder["Database"] = _dbOptions.Value.Database;
            builder["Server"] = _dbOptions.Value.Server;
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
            builder.Password = _dbOptions.Value.Password;
            builder.UserID = _dbOptions.Value.UserId;
            builder.MultipleActiveResultSets = _dbOptions.Value.MultipleActiveResultSets;
            builder["Database"] = null;
            builder["Server"] = _dbOptions.Value.Server;
            return builder.ConnectionString;
        }
    }
}