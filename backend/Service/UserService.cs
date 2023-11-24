using FLCManagementProject.Data;
using FLCManagementProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FLCManagementProject.Service
{
    public class UserService : IUserService
    {
        private readonly FLCMDBContext _context;

        public UserService(FLCMDBContext context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.UserEmail == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public async Task<User> GetUserInfoAsync(string userEmail)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserEmail == userEmail);

            return user;
        }
    }
}
