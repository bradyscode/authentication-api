using authentication_dot_net.Models;
using System.Web.Http;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public interface IUserInterface
    {
        User GetUser(int id);
        string GetUserName(int id);
        Password GetPassword(int id);
        Permission GetPermission(int id);
        object ResetPassword(int id, string password);
        object CreateUser(User user);
        object SetPermission(int id, int permission);
        bool UsernameExists(string username);
    }
}
