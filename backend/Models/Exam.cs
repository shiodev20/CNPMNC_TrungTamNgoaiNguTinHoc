using FLCManagementProject.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class Exam
    {
        [Key] public int examID { get; set; }
        public int languageID { get; set; }
        public int certificateID { get; set; }

        public string examName { get; set; } = string.Empty;

        public string examDesc { get; set; } = string.Empty;

        public string examPrice { get; set; } = string.Empty;

        public Certificate Certificate { get; set; }
        public Language Language { get; set; }

        public ICollection<ExamSession> ExamSessions { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ExamBatch> ExamBatches { get; set; }
    }
}
