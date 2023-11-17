using authentication_dot_net.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Web.Http;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public class UserInterface : IUserInterface
    {
        private IOptions<DatabaseOptions> _dbOptions;

        public UserInterface(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public object CreateUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_dbOptions.Value.Database))
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
    }
}
