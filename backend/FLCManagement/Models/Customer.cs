using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Customer
    {
        [Key] public int customerID { get; set; }
        public string customerFName { get; set; }
        public string customerLName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string customerDob { get; set; }

        public ICollection<ClassDetail> ClassDetails { get; set; }
        public ICollection<CustomerExamShift> CustomerExamShifts { get; set; }
        public ICollection<ExamSessionResult> ExamSessionResults { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
