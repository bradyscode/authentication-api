using authentication_dot_net.Models;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public interface IUserInterface
    {
        User GetUser(int id);
        string GetUserName(int id);
        Password GetPassword(int id);
        Permission GetPermission(int id);

    }
}
