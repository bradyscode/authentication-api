using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace authentication_dot_net.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("/getHashValue")]
        public object Index(string password)
        {

            //salt generation
            var salt = Guid.NewGuid().ToString();
            var saltedPassword = password + salt;

            //generate hash
            var mySHA256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashValue = mySHA256.ComputeHash(inputBytes);

            //store salt and hashedValue
            //when user enters password it will look in db at salt, add it to the salt and compute then compare. Makes sense

            return hashValue;
        }
    }
}
