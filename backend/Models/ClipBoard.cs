using FLCManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagementProject.Models
{
    public class ClipBoard
    {
        [Key]
        public int clipBoardID { get; set; }
        public int customerID { get; set; }
        public int examBatchID { get; set; }
        public int examTypeID { get; set; }
        public float clipBoardPoint { get; set; }
        public bool clipBoardStatus { get; set; }

        public ExamBatch ExamBatch { get; set; }
        public ExamType ExamType { get; set; }
        public Customer Customer { get; set; }
    }
}
