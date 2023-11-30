namespace authentication_class_library.Models
{
    public class BradysAuthenticationSettings
    {
        //public DatabaseOptions DatabaseOptions { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool MultipleActiveResultSets { get; set; }
        //public JwtSettings JwtSettings { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

    }
}