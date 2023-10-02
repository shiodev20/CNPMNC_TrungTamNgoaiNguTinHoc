using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Language
    {
        [Key] public int languageID { get; set; }
        public string languageName { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
