using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Certificate
    {
        [Key] public int certificateID { get; set; }
        public string certificateName { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
