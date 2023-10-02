using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Class
    {
        [Key] public int classID { get; set; }
        public string className { get; set; }
        public int classSize { get; set; }
        public string classStartD { get; set; }
        public string classEndD { get; set; }
        public bool classStatus { get; set; }

        public Branch Branch { get; set; }
        public Course Course { get; set; }

        public ICollection<ClassDetail> ClassDetails { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
