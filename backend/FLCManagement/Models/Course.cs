using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Course
    {
        [Key] public int courseID { get; set; }
        public string courseName { get; set; }
        public string courseDesc { get; set; }
        public int coursePrice { get; set; }

        public Certificate Certificate { get; set; }
        public Language Language { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
