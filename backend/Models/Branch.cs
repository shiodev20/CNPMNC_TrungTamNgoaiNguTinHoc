using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Branch
    {
        [Key] public int branchID { get; set; }
        public string branchName { get; set; } = string.Empty;
        public string branchAddress { get; set; } = string.Empty;

        public ICollection<ExamShift> ExamShifts { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
