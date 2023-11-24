using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class ClassDetail
    {
        [Key] public int classdID { get; set; }
        public int classID { get; set; }
        public int customerID { get; set; }
        public string dayCreate { get; set; }

        public Class Class { get; set; }
        public Customer Customer { get; set; }
    }
}
