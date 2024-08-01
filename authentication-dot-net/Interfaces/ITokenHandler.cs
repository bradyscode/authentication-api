using authentication_dot_net.Models;

namespace authentication_dot_net.Interfaces
{
    public interface ITokenHandler
    {
        string GenerateToken(UserDTO user);
    }
}
