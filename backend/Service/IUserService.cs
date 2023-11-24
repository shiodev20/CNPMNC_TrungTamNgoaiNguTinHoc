using FLCManagementProject.Models;

namespace FLCManagementProject.Service
{
    public interface IUserService
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        Task<User> GetUserInfoAsync(string userEmail);
    }
}
