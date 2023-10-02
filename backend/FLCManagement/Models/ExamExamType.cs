using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamExamType
    {
        [Key] public int examExamTypeID { get; set; }

        public Exam Exam { get; set; }
        public ExamType ExamType { get; set; }
    }
}
