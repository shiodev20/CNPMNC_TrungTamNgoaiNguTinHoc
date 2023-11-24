using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Course
    {
        [Key] public int courseID { get; set; }
        public int languageID { get; set; }
        public int certificateID { get; set; }
        public string courseName { get; set; } = string.Empty;
        public string courseDesc { get; set; } = string.Empty;
        public int coursePrice { get; set; }

        public Certificate Certificate { get; set; }
        public Language Language { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
