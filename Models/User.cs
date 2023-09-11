using MKExpress.API.Constants;
using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{

    public class User : BaseModel
    {
        [Required] public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Position { get; set; }
        [Required] public string UserName { get; set; }

        [EmailAddress(ErrorMessage = StaticValues.InvalidEmail)]
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public List<UserPermission> UserPermissions { get; set; }
    }
}