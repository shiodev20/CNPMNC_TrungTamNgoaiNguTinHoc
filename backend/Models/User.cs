using FLCManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagementProject.Models
{
    public class User
    {
        [Key] public int UserID { get; set; }
        public string UserFName { get; set; } = string.Empty;
        public string UserLName { get; set; } = string.Empty;
        public string UserFullName => (UserFName ?? string.Empty) + (UserLName ?? string.Empty);
        public string UserDob { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Thêm thuộc tính RoleID để quản lý quyền của người dùng
        public int RoleID { get; set; }

        public Role Role { get; set; }
    }
}
