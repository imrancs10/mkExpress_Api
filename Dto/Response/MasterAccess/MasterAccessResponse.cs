using System.Collections.Generic;

namespace MKExpress.Web.API.Dto.Response.MasterAccess
{
    public class MasterAccessResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public string RoleName { get; set; }
        public string MenuName { get; set; }
        public List<MasterAccessDetailResponse> MasterAccessDetails { get; set; }
    }
}
