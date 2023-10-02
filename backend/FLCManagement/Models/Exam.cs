using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Exam
    {
        [Key] public int examID { get; set; }
        public string examName { get; set; }
        public string examDesc { get; set; }
        public int examPrice { get; set; }

        public Certificate Certificate { get; set; }
        public Language Language { get; set; }

        public ICollection<ExamExamType> ExamExamTypes { get; set; }
        public ICollection<ExamSession> ExamSessions { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
