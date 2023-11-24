using FLCManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace FLCManagementProject.Models
{
    public class BillCusClass
    {
        [Key]
        public int billCusClassID { get; set; }
        public int classID { get; set; }
        public int customerID { get; set; }
        public DateTime billCusClassDayCreate { get; set; }
        public int billCusClassPrice { get; set; }

        public Class classes { get; set; }
        public Customer customer { get; set; }
    }
}
