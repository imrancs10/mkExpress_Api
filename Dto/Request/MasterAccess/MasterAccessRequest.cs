using MKExpress.Web.API.Dto.Response.MasterAccess;
using System.Collections.Generic;

namespace MKExpress.Web.API.Dto.Request.MasterAccess
{
    public class MasterAccessRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<MasterAccessDetailRequest> MasterAccessDetails { get; set; }
    }
}
