using System.Security.Cryptography;
using System.Text;

namespace authentication_dot_net.Models
{
    public class User
    {

        public User(string username, string password, int permission)
        {
            Username = username;
            Password = HashPassword(password);
            Permission = (Permission)permission;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public Password Password { get; set; }
        public Permission Permission { get; set; }

        private Password HashPassword(string password)
        {
            IsPasswordComplexEnough(password);
            //salt generation
            var salt = Guid.NewGuid().ToString();
            var saltedPassword = password + salt;

            //generate hash
            var mySHA256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashValue = mySHA256.ComputeHash(inputBytes);

            //store salt and hashedValue
            //when user enters password it will look in db at salt, add it to the salt and compute then compare. Makes sense
            return new Password(hashValue, salt);
        }

        private bool IsPasswordComplexEnough(string password)
        {
            if (password.Length < 8)
            {
                throw new Exception("Password is less than 8 characters");
            }

            if (!password.Any(char.IsDigit))
            {
                throw new Exception("Password does not contain a digit");
            }

            if (!password.Any(char.IsUpper))
            {
                throw new Exception("Password does not contain an uppercase letter");
            }

            if (!password.Any(char.IsLower))
            {
                throw new Exception("Password does not contain a lowercase letter");
            }

            if (!password.Any(char.IsSymbol))
            {
                throw new Exception("Password does not contain a special character");
            }

            return true;
        }
    }
}
