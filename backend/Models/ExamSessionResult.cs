using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamSessionResult
    {
        [Key] public int examsrID { get; set; }
        public int customerID { get; set; }
        public int examsID { get; set; }
        public float examsrResult { get; set; }

        public Customer Customer { get; set; }
        public ExamSession ExamSession { get; set; }
    }
}
