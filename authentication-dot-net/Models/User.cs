namespace authentication_dot_net.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Password Password { get; set; }
        public Permission Permission { get; set; }
    }
}
