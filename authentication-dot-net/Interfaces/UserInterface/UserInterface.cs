using authentication_dot_net.Models;

namespace authentication_dot_net.Interfaces.UserInterface
{
    public class UserInterface : IUserInterface
    {
        public Password GetPassword(int id)
        {
            throw new NotImplementedException();
        }

        public Permission GetPermission(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int id)
        {
            throw new NotImplementedException();
        }
    }
}
