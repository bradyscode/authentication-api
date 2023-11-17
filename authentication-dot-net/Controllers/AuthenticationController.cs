using authentication_dot_net.Interfaces.UserInterface;
using authentication_dot_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace authentication_dot_net.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserInterface _userInterface;

        public AuthenticationController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpGet("/CreateUser")]
        public object CreateUser(string username, string password, int permission)
        {
            var user = new User(username, password, permission);
            var returnValue = _userInterface.CreateUser(user);
            return returnValue;
        }
    }
}
