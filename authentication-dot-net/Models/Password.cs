namespace authentication_dot_net.Models
{
    public class Password
    {
        public string Salt { get; set; }
        public string HashValue { get; set; }
    }
}
