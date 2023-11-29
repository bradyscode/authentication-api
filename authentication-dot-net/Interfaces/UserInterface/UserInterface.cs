
using authentication_dot_net.Models;
using authentication_dot_net.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public class UserInterface : IUserInterface
    {
        private IOptions<DatabaseOptions> _dbOptions;
        private ConnectionStringBuilder _connectionStringBuilder;

        public UserInterface(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions;
            _connectionStringBuilder = new ConnectionStringBuilder(dbOptions);
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStringBuilder.BuildConnectionString()))
                {
                    await connection.OpenAsync();

                    var sql = "SELECT Salt, HashValue FROM Users WHERE Username = @Username";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Username", username);

                    var saltAndHash = await connection.QueryFirstAsync<Password>(sql, parameters);
                    var hashedPassword = HashPassword(saltAndHash, password);

                    if (hashedPassword.HashValue.SequenceEqual(saltAndHash.HashValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Password HashPassword(Password saltAndHash, string password)
        {
            var salt = saltAndHash.Salt;

            //generate hash
            var mySHA256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashValue = mySHA256.ComputeHash(inputBytes);

            return new Password(hashValue, salt);
        }

        public object CreateUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStringBuilder.BuildConnectionString()))
                {
                    connection.Open();

                    var sql = "INSERT INTO Users (Username, Salt, HashValue, Permission) VALUES (@Username, @Salt, @HashValue, @Permission)";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Username", user.Username);
                    parameters.Add("@Salt", user.Password.Salt);
                    parameters.Add("@HashValue", user.Password.HashValue);
                    parameters.Add("@Permission", (int)user.Permission);

                    connection.Execute(sql, parameters);
                }
                return "User Created";
            }catch (Exception ex)
            {
                throw ex; 
            }

        }

        public Password GetPassword(int id)
        {
            throw new NotImplementedException();
        }

        public Permission GetPermission(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int id)
        {
            throw new NotImplementedException();
        }

        public object ResetPassword(int id, string password)
        {
            throw new NotImplementedException();
        }

        public object SetPermission(int id, int permission)
        {
            throw new NotImplementedException();
        }

        public bool UsernameExists(string username)
        {
            using (var connection = new SqlConnection(_dbOptions.Value.Database))
            {
                connection.Open();

                var sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);

                int affectedRows = connection.Execute(sql, parameters);
                return affectedRows > 0;
            }
        }

    }
}
