namespace authentication_class_library.Models
{
    public class DatabaseOptions
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool MultipleActiveResultSets { get; set; }
    }
}