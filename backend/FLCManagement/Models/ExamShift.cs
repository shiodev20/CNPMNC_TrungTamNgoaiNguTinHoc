using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamShift
    {
        [Key] public int examstID { get; set; }
        public int examstSize { get; set; }
        public string examstStartH { get; set; }
        public string examstEndH { get; set; }
        public string exemstDate { get; set; }

        public Branch Branch { get; set; }
        public ExamType ExamType { get; set; }
        public ExamSession ExamSession { get; set; }

        public ICollection<CustomerExamShift> CustomerExamShifts { get; set; }
    }
}
