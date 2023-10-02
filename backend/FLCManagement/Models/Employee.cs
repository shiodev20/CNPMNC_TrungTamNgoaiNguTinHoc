using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Employee
    {
        [Key] public int employeeID { get; set; }
        public string employeeFName { get; set; }
        public string employeeLName { get; set; }
        public string employeeEmail { get; set; }
        public string employeePhone { get; set; }
        public string employeePassword { get; set; }

        public Role Role { get; set; }
    }
}
