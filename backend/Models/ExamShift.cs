using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamShift
    {
        [Key] public int examstID { get; set; }
        public int branchID { get; set; }
        public int examtID { get; set; }
        public int examsID { get; set; }
        public int examstSize { get; set; }
        public string examstStartH { get; set; } = string.Empty;
        public string examstEndH { get; set; } = string.Empty;
        public string examstDate { get; set; } = string.Empty;

        public Branch Branch { get; set; }
        public ExamType ExamType { get; set; }
        public ExamSession ExamSession { get; set; }

        public ICollection<CustomerExamShift> CustomerExamShifts { get; set; }
    }
}
