using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamType
    {
        [Key] public int examtID { get; set; }
        public string examtName { get; set; }

        public ICollection<ExamExamType> ExamExamTypes { get; set; }
        public ICollection<ExamShift> ExamShifts { get; set; }
    }
}
