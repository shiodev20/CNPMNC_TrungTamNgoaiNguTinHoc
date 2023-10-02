using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Lesson
    {
        [Key] public int lessonID { get; set; }
        public string lessonStartD { get; set; }
        public string lessonEndD { get; set ; }
        public string lessonShift { get; set; }
        public string lessonStartH { get; set; }
        public string lessonEndH { get; set; }

        public Class Class { get; set; }
    }
}
