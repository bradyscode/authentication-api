using authentication_dot_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public class UserInterface : IUserInterface
    {
        public IHttpActionResult CreateUser(User user)
        {
            throw new NotImplementedException();
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

        public IHttpActionResult ResetPassword(int id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
