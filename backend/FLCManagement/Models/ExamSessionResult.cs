using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamSessionResult
    {
        [Key] public int examsrID { get; set; }
        public bool examsrResult { get; set; }

        public Customer Customer { get; set; }
        public ExamSession ExamSession { get; set; }
    }
}
