using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamSession
    {
        [Key] public int examsID { get; set; }
        public string examsName { get; set; }
        public bool examsStatus { get; set; }

        public Exam Exam { get; set; }

        public ICollection<ExamShift> ExamShifts { get; set; }
        public ICollection<ExamSessionResult> ExamSessionResults { get; set; }
    }
}
