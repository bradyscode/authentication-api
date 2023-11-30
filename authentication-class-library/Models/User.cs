using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace authentication_class_library.Models
{
    public class User
    {
        private static readonly Regex symbolRegex = new Regex("[!@#$%^&*()_+-=[]{};':\",./<>?]+");

        public User(string username, string password)
        {
            Username = username;
            Password = HashPassword(password);
            Permission = Permission.USER;
        }

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
                throw new ArgumentException("Password must be at least 8 characters long.");
            }

            bool hasDigit = false;
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasSpecialCharacter = false;

            foreach (char character in password)
            {
                if (char.IsDigit(character))
                {
                    hasDigit = true;
                }
                else if (char.IsUpper(character))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(character))
                {
                    hasLowercase = true;
                }
                else if (!char.IsLetterOrDigit(character))
                {
                    hasSpecialCharacter = true;
                }
            }

            if (!hasDigit)
            {
                throw new ArgumentException("Password must contain at least one digit.");
            }

            if (!hasUppercase)
            {
                throw new ArgumentException("Password must contain at least one uppercase letter.");
            }

            if (!hasLowercase)
            {
                throw new ArgumentException("Password must contain at least one lowercase letter.");
            }

            if (!hasSpecialCharacter)
            {
                throw new ArgumentException("Password must contain at least one special character.");
            }

            return true;
        }
    }
}
