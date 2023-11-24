using FLCManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagementProject.Models
{
    public class ExamBatch
    {
        [Key] public int examBatchID { get; set; }
        public int examID { get; set; }
        public string examBatchName { get; set; }
        public bool examBatchStatus { get; set; }

        public Exam Exam { get; set; }

        public ICollection<ClipBoard> ClipBoards { get; set; }
    }
}
