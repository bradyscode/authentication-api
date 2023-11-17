namespace authentication_dot_net.Models
{
    /// <summary>
    /// Password object for authentication, contains salt and hashed password + salt
    /// </summary>
    public class Password
    {
        public Password()
        {
            //here for when pulling and parsing from db
        }
        public Password(byte[] hashValue, string salt)
        {
            HashValue = hashValue;
            Salt = salt;
        }

        public string Salt { get; set; }
        public byte[] HashValue { get; set; }
    }
}
