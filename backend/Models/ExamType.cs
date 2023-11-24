using FLCManagementProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ExamType
    {
        [Key] public int examtID { get; set; }
        public string examtName { get; set; }

        public ICollection<ExamShift> ExamShifts { get; set; }
        public ICollection<ExamBatch> ExamBatches { get; set; }
    }
}
