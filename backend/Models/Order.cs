using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Order
    {
        [Key] public int orderID { get; set; }
        public int courseID { get; set; }
        public int examID { get; set; }
        public int customerID { get; set; }
        public string orderDesc { get; set; }
        public int orderTotal { get; set; }
        public string orderCreatedAt { get; set; }
        
        public Course Course { get; set; }
        public Exam Exam { get; set; }
        public Customer Customer { get; set; }

        
    }
}
