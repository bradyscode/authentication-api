
using authentication_dot_net.Models;
using authentication_dot_net.Services;
using Dapper;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;

namespace UnitTests.Middleware
{
    public class DatabaseMiddlewareTests
    {
        private DatabaseSetup middleware;
        private Mock<IOptions<DatabaseOptions>> mockDbOptions;

        [SetUp]
        public void Setup()
        {
            mockDbOptions = new Mock<IOptions<DatabaseOptions>>();
            middleware = new DatabaseSetup(mockDbOptions.Object);
        }

        [Test]
        public void CheckDatabaseExistsAndCreateDatabase_ShouldCreateDatabaseIfNotExists()
        {
            // Arrange
            mockDbOptions.Setup(m => m.Value.Database).Returns("MyDatabase");
            const string connectionString = "Server=localhost;Database=master;Integrated Security=True";
            var mockConnection = new Mock<SqlConnection>(connectionString);

            // Act
            middleware.CheckDatabaseExistsAndCreateDatabase();

            // Assert
            mockConnection.Verify(m => m.Execute("CREATE DATABASE MyDatabase"));
        }

        [Test]
        public void CheckDatabaseExistsAndCreateDatabase_ShouldCreateTableIfNotExists()
        {
            // Arrange
            mockDbOptions.Setup(m => m.Value.Database).Returns("MyDatabase");
            const string connectionString = "Server=localhost;Database=MyDatabase;Integrated Security=True";
            var mockConnection = new Mock<SqlConnection>(connectionString);
            mockConnection.Setup(m => m.QuerySingleOrDefault<bool>(It.IsAny<string>(), It.IsAny<object>())).Returns(true);

            // Act
            middleware.CheckDatabaseExistsAndCreateDatabase();

            // Assert
            mockConnection.Verify(m => m.Execute("CREATE TABLE Users"));
        }

        [Test]
        public void DatabaseExists_ShouldReturnTrueIfDatabaseExists()
        {
            // Arrange
            const string connectionString = "Server=localhost;Database=MyDatabase;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("CREATE DATABASE MyDatabase");
                connection.Close();
            }

            // Act
            var result = middleware.DatabaseExists(connectionString, "MyDatabase");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DatabaseExists_ShouldReturnFalseIfDatabaseDoesNotExist()
        {
            // Arrange
            const string connectionString = "Server=localhost;Database=Master;Integrated Security=True";

            // Act
            var result = middleware.DatabaseExists(connectionString, "MyDatabase");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TableExists_ShouldReturnTrueIfTableExists()
        {
            // Arrange
            const string connectionString = "Server=localhost;Database=MyDatabase;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("CREATE TABLE Users");
                connection.Close();
            }

            // Act
            var result = middleware.TableExists(connectionString, "Users");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TableExists_ShouldReturnFalseIfTableDoesNotExist()
        {
            // Arrange
            const string connectionString = "Server=localhost;Database=MyDatabase;Integrated Security=True";

            // Act
            var result = middleware.TableExists(connectionString, "Users");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
