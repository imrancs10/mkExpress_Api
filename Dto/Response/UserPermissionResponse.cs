using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class UserPermissionResponse
    {
        public int Id { get; set; }
        public List<int> RoleId { get; set; }
        public int PermissionResourceId { get; set; }
        public string PermissionResourceName { get; set; }
        public string PermissionResourceCode { get; set; }
        public string PermissionResourceType { get; set; }
    }
}
