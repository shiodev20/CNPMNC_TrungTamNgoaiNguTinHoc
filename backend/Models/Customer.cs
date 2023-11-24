using FLCManagementProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Customer
    {
        [Key] public int customerID { get; set; }
        public string UserFName { get; set; } = string.Empty;
        public string UserLName { get; set; } = string.Empty;
        public string UserFullName => (UserFName ?? string.Empty) + (UserLName ?? string.Empty);
        public string UserDob { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public int roleID { get; set; }

        public ICollection<ClassDetail> ClassDetails { get; set; }
        public ICollection<CustomerExamShift> CustomerExamShifts { get; set; }
        public ICollection<ExamSessionResult> ExamSessionResults { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<BillCusClass> BillCusClasses { get; set; }
        public ICollection<ClipBoard> ClipBoards { get; set; }
    }
}
