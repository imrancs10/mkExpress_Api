using MKExpress.API.Models;
using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.Web.API.Models
{
    public class MasterAccess:BaseModel
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ResetCode { get; set; }
        public string ResetCodeExpire { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; }

        public List<MasterAccessDetail> MasterAccessDetails { get; set; }
    }
}
