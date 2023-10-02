using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Role
    {
        [Key] public int roleID { get; set; }
        public string roleName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
