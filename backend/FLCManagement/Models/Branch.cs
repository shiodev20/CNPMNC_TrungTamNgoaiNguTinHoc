using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Branch
    {
        [Key] public int branchID { get; set; }
        public string branchName { get; set; }
        public string branchAddress { get; set; }

        public ICollection<ExamShift> ExamShifts { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
