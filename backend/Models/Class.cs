using FLCManagementProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Class
    {
        [Key] public int classID { get; set; }
        public int branchID { get; set; }
        public int courseID { get; set; }
        public string className { get; set; } = string.Empty;
        public int classSize { get; set; }
        public string classStartD { get; set; } = string.Empty;
        public string classEndD { get; set; } = string.Empty;
        public bool classStatus { get; set; }

        public Branch Branch { get; set; }
        public Course Course { get; set; }

        public ICollection<ClassDetail> ClassDetails { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<BillCusClass> BillCusClasses { get; set; }
    }
}
