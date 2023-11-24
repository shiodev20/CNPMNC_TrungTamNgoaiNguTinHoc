using FLCManagementProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Employee
    {
        [Key] public int employeeID { get; set; }
        public int roleID { get; set; }
        public required string employeeFName { get; set; } = string.Empty;
        public required string employeeLName { get; set; } = string.Empty;
        public required string employeeEmail { get; set; } = string.Empty;
        public required string employeePhone { get; set; } = string.Empty;
        public required string employeeDob { get; set; } = string.Empty;
        public required string employeePassword { get; set; } = string.Empty;

        public Role Role { get; set; }
    }
}
