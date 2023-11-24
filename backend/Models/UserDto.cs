    using System.ComponentModel.DataAnnotations;

    namespace FLCManagementProject.Models
    {
        public class UserDto
        {
            public required string userFName { get; set; }
            public required string userLName { get; set; }
            public required string userEmail { get; set; }
            public required string userPassword { get; set; }
            public required string userPhone { get; set; }
            public required string userDob { get; set; }
        }
    }
